package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

public class LightApiServiceImpl implements LightApiService {

    @Override
    public List<Light> getAllLightsInNetwork() {
        return new ArrayList<Light>();
    }
}
