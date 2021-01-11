package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.dto.CreateLightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.*;

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
        TimerTask timerTask = new TimerTask() {
            @Override
            public void run() {
                List<Light> lights = lightDao.findAll();
                for (Light light : lights){
                    if (light.getOnTimer() != null && !light.getOnState()) {
                        LocalDateTime lightTime = light.getOnTimer();
                        if (lightTime.getMinute() == LocalDateTime.now().getMinute() && lightTime.getHour() == LocalDateTime.now().getHour()){
                            light.setOnState(true);
                            lightDao.save(light);
                            System.out.println("light with Id " + light.getId() + " was turned on");
                        }
                    }
                }
            }
        };
        Timer timer = new Timer(true);
        timer.scheduleAtFixedRate(timerTask, 0, 30 * 1000);
    }
}
