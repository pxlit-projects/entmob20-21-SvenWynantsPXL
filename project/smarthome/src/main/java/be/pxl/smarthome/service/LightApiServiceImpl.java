package be.pxl.smarthome.service;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiMap;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.api.*;
import org.springframework.stereotype.Service;

import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class LightApiServiceImpl implements LightApiService {

    private final LightApiMap lightApiMap;

    public LightApiServiceImpl(LightApiMap lightApiMap) {
        this.lightApiMap = lightApiMap;
    }

    @Override
    public List<Light> getAllLightsInNetwork() {
        return lightApiMap.getAll().stream()
                .map(LightApi::getAllLights)
                .flatMap(Collection::stream)
                .collect(Collectors.toList());
    }

    @Override
    public void addLight(Light light) {
        getLightApiFor(light).addLight(light);
    }

    @Override
    public void removeLight(Light light) {
        final var lightApi = getLightApiFor(light);
        lightApi.removeLight(light.getName());
    }

    @Override
    public void flipSwitch(Light light) {
        final var lightApi = getLightApiFor(light);
        lightApi.changeState(light);
    }

    @Override
    public void turnOnLight(Light light) {
        final var lightApi = getLightApiFor(light);
        lightApi.turnOnLight(light);
    }

    @Override
    public void turnOffLight(Light light) {
        final var lightApi = getLightApiFor(light);
        lightApi.turnOffLight(light);
    }

    @Override
    public void updateLight(Light light) {
        final var lightApi = getLightApiFor(light);
        lightApi.updateLight(light);
    }

    private LightApi getLightApiFor(Light light) {
        return lightApiMap.get(light);
    }
}
