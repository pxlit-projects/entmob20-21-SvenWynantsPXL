package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.api.DummyApi;
import be.pxl.smarthome.service.api.IkeaApi;
import be.pxl.smarthome.service.api.PhilipsApi;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class LightApiServiceImpl implements LightApiService {
    private DummyApi dummyApi;
    private IkeaApi ikeaApi;
    private PhilipsApi philipsApi;

    @Override
    public List<Light> getAllLightsInNetwork() {
        List<Light> allLights = new ArrayList<>();
        allLights = dummyApi.getAllLights();
        allLights = Stream.concat(allLights.stream(), ikeaApi.getAllLights().stream()).collect(Collectors.toList());
        
        return allLights;
    }
}
