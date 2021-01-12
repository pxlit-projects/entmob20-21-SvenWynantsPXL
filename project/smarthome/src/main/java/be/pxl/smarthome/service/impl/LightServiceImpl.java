package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.dto.CreateLightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.LightService;
import org.apache.tomcat.jni.Local;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.stream.Stream;

@Service
public class LightServiceImpl implements LightService {
    @Autowired
    private LightService service;
    @Autowired
    private LightDao lightDao;
    @Autowired
    private LightGroupDao groupDao;
    @Autowired
    private LightApiService lightApiService;

    public Light addLight(CreateLightDto createLightDto) {
        Light light = new Light();

        light.setName(createLightDto.Name);
        light.setManufacturer(createLightDto.LightManufacturer);
        light.setOnState(false);
        light.setType(createLightDto.Type);
        light.setBrightness(100);
        light.setOnTimer(null);
        light.setOnSunDown(false);
        light = lightDao.save(light);
        lightApiService.addLight(light);

        if(createLightDto.LightGroupId != 0) {
            if (groupDao.findById(createLightDto.LightGroupId).isPresent()){
                LightGroup group = groupDao.findById(createLightDto.LightGroupId).get();
                List<Light> groupLights = group.getLights();
                if (groupLights == null || groupLights.size() == 0){
                    groupLights = new ArrayList<>();
                }
                groupLights.add(light);
                group.setLights(groupLights);
                groupDao.save(group);
            }
        }

        return light;
    }

    //To import the lights from the bridges and dummy to this database
    @Override
    public Light addLight(Light light) {
        lightDao.save(light);
        return light;
    }

    public List<Light> getAllLights(){
        return lightDao.findAll();
    }

    @Override
    public void removeLight(Light light) {
        lightDao.delete(light);
        lightApiService.removeLight(light);
    }

    @Override
    public Light flipSwitch(Light light) {
        light.setOnState(!light.getOnState());
        lightDao.save(light);
        lightApiService.flipSwitch(light);
        return light;
    }

    @Override
    public Light updateLight(Light light) {
        light = lightDao.save(light);
        lightApiService.updateLight(light);
        return light;
    }

    @Override
    public Optional<Light> findLightById(Integer id) {
        return Optional.ofNullable(id)
                // if id is null => wont execute
                .flatMap(lightDao::findById);
    }

    @PostConstruct
    public void addLightsFromBridges(){
        //Check if lights are in service
        List<Light> lights = service.getAllLights();
        List<Light> apiLights = lightApiService.getAllLightsInNetwork();
        int counter = 0;
        for (Light light : apiLights) {
            if (lights == null) {
                lights = new ArrayList<>();
            }
            if (lights.size() == 0) {
                lights.add(light);
                service.addLight(light);
            } else {
                Light check = new Light();
                boolean exists = false;
                for (int i = 0; i < lights.size(); i++) {
                    if ((lights.get(i).getName().equals(light.getName()))) {
                        exists = true;
                    } else {
                        check = light;
                    }
                }
                if (!exists) {
                    lights.add(check);
                    service.addLight(check);

                    if (counter < 2) {
                        LightGroup group = groupDao.findById(1).orElse(null);
                        List<Light> groupLights;
                        if (group != null) {
                            groupLights = group.getLights();
                            if (groupLights == null){
                                groupLights = new ArrayList<>();
                            }
                            groupLights.add(check);
                            group.setLights(groupLights);
                            groupDao.save(group);
                        }
                    }
                    counter ++;
                }
            }
        }
    }

    @PostConstruct
    public void timerToCheckOnTime(){
        final LocalDateTime[] sunsetTime = {LocalDateTime.of(2020, 1, 1, 0, 0)};

        TimerTask checkTimers = new TimerTask() {
            @Override
            public void run() {
                LocalDateTime now = LocalDateTime.now().withNano(0);

                List<Light> lights = lightDao.findAll();
                for (Light light : lights){
                    if (light.getOnTimer() != null && !light.isOnSunDown()) {
                        LocalDateTime lightTime = light.getOnTimer().withNano(0).withMonth(now.getMonthValue()).withYear(now.getYear()).withDayOfMonth(now.getDayOfMonth());
                        if (lightTime.isEqual(now) || lightTime.plusMinutes(31).isAfter(now)){
                            lightUpdater(light);
                        }
                    }
                    if (sunsetTime[0].isEqual(now) || sunsetTime[0].plusMinutes(31).isAfter(now)){
                        if (light.isOnSunDown()){
                            lightUpdater(light);
                        }
                    }
                }
            }
        };
        Timer timer = new Timer(true);
        timer.scheduleAtFixedRate(checkTimers, 0, 18 * 1000);

        TimerTask sunDownCheck = new TimerTask() {
            @Override
            public void run() {
                if (LocalDateTime.now().getHour() % 2 == 0){
                    sunsetTime[0] = getSunsetTime();
                }
            }
        };
        Timer checkDailySunDown = new Timer(true);
        long hourPeriod = 1000L * 60L * 60L;
        checkDailySunDown.scheduleAtFixedRate(sunDownCheck, 0, hourPeriod);
    }

    private void lightUpdater(Light light) {
        if (light.getOnState() && light.getBrightness() != 100){
            light.setBrightness(light.getBrightness() + 1);
            updateLight(light);
        } else if (!light.getOnState()) {
            light.setOnState(true);
            light.setBrightness(1);
            updateLight(light);
        }
    }

    private LocalDateTime getSunsetTime(){
        HttpClient client = HttpClient.newHttpClient();
        var request = HttpRequest.newBuilder(
                URI.create("https://api.sunrise-sunset.org/json?lat=50.9303735&lng=5.3378043&date=today"))
                .header("accept", "application/json")
                .build();

        try {
            var response = client.send(request, HttpResponse.BodyHandlers.ofLines()).body().findFirst().get();

            int startIndex = response.indexOf("sunset") + 8;
            String sunsetHour = response.substring(startIndex, startIndex + 9).replace("\"", "").trim();
            LocalDateTime today = LocalDateTime.now();
            int [] sunsetTime = Arrays.stream(sunsetHour.split(":")).mapToInt(Integer::parseInt).toArray();
            LocalDateTime sunset = LocalDateTime.of(today.getYear(), today.getMonthValue(), today.getDayOfMonth(), sunsetTime[0] + 13, sunsetTime[1], sunsetTime[2]).minusMinutes(30).withNano(0);
            System.out.println(sunset);
            return sunset;
        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
            return null;
        }
    }
}
