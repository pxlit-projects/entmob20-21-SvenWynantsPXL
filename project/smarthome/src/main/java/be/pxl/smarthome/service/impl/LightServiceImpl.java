package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class LightServiceImpl implements LightService {

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

    @Override
    public Light getLightById(Integer lightId) {
        Optional<Light> optLight = dao.findById(lightId);
        return optLight.orElse(null);
    }

    public List<Light> getAllLights(){
        List<Light> lights = dao.findAll();

        return lights;
    }

    @Override
    public void removeLight(Light light) {
        dao.delete(light);
    }
}
