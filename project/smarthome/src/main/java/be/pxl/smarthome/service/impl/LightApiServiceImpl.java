package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.api.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Service
public class LightApiServiceImpl implements LightApiService {

    private LightApi lightApi;
    @Autowired
    private DummyApi dummyApi;
    @Autowired
    private IkeaApi ikeaApi;
    @Autowired
    private PhilipsApi philipsApi;

    @Override
    public List<Light> getAllLightsInNetwork() {
        List<Light> allLights = new ArrayList<>();
        allLights = dummyApi.getAllLights();
        allLights = Stream.concat(allLights.stream(), ikeaApi.getAllLights().stream()).collect(Collectors.toList());
        allLights = Stream.concat(allLights.stream(), philipsApi.getAllLights().stream()).collect(Collectors.toList());
        return allLights;
    }

    @Override
    public void addLight(Light light) {
        lightApi = checkManufacturer(light.getManufacturerName());

        lightApi.addLight(light);
    }

    private LightApi checkManufacturer(String name) {
        if (name.toLowerCase().equals("ikea")) {
            return ikeaApi;
        } else if (name.toLowerCase().equals("philips")){
            return philipsApi;
        } else {
            return dummyApi;
        }
    }
}
