package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;

import java.util.ArrayList;
import java.util.List;

public class DummyApi {
    private List<Light> lights;
    public DummyApi() {
        lights = new ArrayList<>();
        Light light1 = new Light(100, false, "dummy light", "Dummy light 1", "Dummy");
        Light light2 = new Light(100, false, "dummy light", "Dummy light 2", "Dummy");
        lights.add(light1);
        lights.add(light2);
    }

    public List<Light> getAllLights(){
        return lights;
    }
}
