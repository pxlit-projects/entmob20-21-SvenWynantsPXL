package be.pxl.smarthome.service;

import be.pxl.smarthome.models.Light;
import java.util.List;

public interface LightApiService {
    List<Light> getAllLightsInNetwork();
    void addLight(Light light);
    void removeLight(Light light);
    void flipSwitch(Light light);
    void turnOnLight(Light light);
    void turnOffLight(Light light);
    void removeGroup(Light light);
}
