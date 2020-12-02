package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface LightApi {
    List<Light> getAllLights();
    void addLight(Light light);
    void removeLight(String name);
    Light getLightByName(String name);
    void changeState(Light light);
}
