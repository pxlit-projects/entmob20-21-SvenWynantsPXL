package be.pxl.smarthome.service;

import be.pxl.smarthome.dto.CreateLightDto;
import be.pxl.smarthome.models.Light;

import java.util.List;
import java.util.Optional;

public interface LightService {
    Light addLight(CreateLightDto light);
    Light addLight(Light light);
    List<Light> getAllLights();
    Optional<Light> findLightById(Integer id);
    void removeLight(Light light);
    Light flipSwitch(Light light);
    Light updateLight(Light light);
}
