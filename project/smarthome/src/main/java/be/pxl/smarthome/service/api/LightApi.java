package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightManufacturer;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface LightApi {
    LightManufacturer getManufacturer();
    List<Light> getAllLights();
    void addLight(Light light);
    void removeLight(String name);
    Light getLightByName(String name);
    void changeState(Light light);
    void turnOnLight(Light light);
    void turnOffLight(Light light);
    void updateLight(Light light);
}
