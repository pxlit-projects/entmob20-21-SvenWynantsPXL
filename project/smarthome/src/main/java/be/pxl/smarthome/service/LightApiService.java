package be.pxl.smarthome.service;

import be.pxl.smarthome.models.Light;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface LightApiService {
    List<Light> getAllLightsInNetwork();
}
