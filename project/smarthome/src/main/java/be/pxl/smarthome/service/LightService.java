package be.pxl.smarthome.service;

import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

public interface LightService {
    Light addLight(LightDto light);
    Light addLight(Light light);
    List<Light> getAllLights();
    Optional<Light> findLightById(Integer id);
    void removeLight(Light light);
    Light flipSwitch(Light light);
    void removeGroup(Light light);
}
