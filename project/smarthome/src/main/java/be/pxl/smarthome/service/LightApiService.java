package be.pxl.smarthome.service;

import be.pxl.smarthome.models.Light;
import org.springframework.stereotype.Service;

import java.util.List;

public interface LightApiService {
    List<Light> getAllLightsInNetwork();
    void addLight(Light light);
}
