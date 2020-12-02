package be.pxl.smarthome.service;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightManufacturer;
import be.pxl.smarthome.service.api.LightApi;
import org.springframework.stereotype.Component;

import java.util.Collection;
import java.util.Map;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;

@Component
public class LightApiMap {
    public final Map<LightManufacturer, LightApi> lightApiMap;
    private final LightApi defaultLightApi;

    public LightApiMap(Set<LightApi> lightApis) {
        lightApiMap = lightApis.stream()
                .collect(Collectors.toUnmodifiableMap(LightApi::getManufacturer, Function.identity()));
        defaultLightApi = lightApiMap.get(LightManufacturer.DUMMY);
    }

    public LightApi get(Light light) {
        return lightApiMap.getOrDefault(light.getManufacturer(), defaultLightApi);
    }

    public Collection<LightApi> getAll(){
        return lightApiMap.values();
    }
}
