package be.pxl.smarthome.service;

import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import org.springframework.stereotype.Service;

import java.util.List;

public interface LightService {
    //POST
    Light addLight(LightDto light);
    Light addLight(Light light);
    //GET
    Light getLightById(Integer lightId);
    List<Light> getAllLights();
    //DELETE
    void removeLight(Light light);
    //PUT
    Light flipSwitch(Light light);
}
