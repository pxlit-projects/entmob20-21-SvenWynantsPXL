package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class LightServiceImpl implements LightService {

    @Autowired
    private LightService service;
    @Autowired
    private LightDao dao;
    @Autowired
    private LightApiService lightApiService;

    public Light addLight(LightDto lightDto) {
        Light light = new Light();

        light.setName(lightDto.name);
        light.setManufacturerName(lightDto.manufacturerName);
        light.setOnState(false);
        light.setType(lightDto.type);
        if (!lightDto.manufacturerName.equals("")) {
            //TODO: lightgroup has no service yet
        }

        dao.save(light);
        lightApiService.addLight(light);

        return light;
    }

    //To import the lights from the bridges and dummy to this database
    @Override
    public Light addLight(Light light) {
        dao.save(light);
        return light;
    }

    public List<Light> getAllLights(){
        List<Light> lights = dao.findAll();

        return lights;
    }

    @Override
    public void removeLight(Light light) {
        dao.delete(light);
        lightApiService.removeLight(light);
    }

    @Override
    public Light flipSwitch(Light light) {
        light.setOnState(!light.getOnState());
        dao.save(light);
        lightApiService.flipSwitch(light);
        return light;
    }

    @Override
    public Optional<Light> findLightById(Integer id) {
        return Optional.ofNullable(id)
                // if id is null => wont execute
                .flatMap(dao::findById);
    }

    @PostConstruct
    public void addLightsFromBridges(){
        //Check if lights are in service
        List<Light> lights = service.getAllLights();
        List<Light> apiLights = lightApiService.getAllLightsInNetwork();

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
                }
            }
        }
    }
}
