package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;

public class DummyApi {
    private List<Light> lights;
    private static Logger logger = LogManager.getLogger(DummyApi.class);

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

    public void addLight(Light light) {
        lights.add(light);
        logger.debug("Light added to dummy datalist");
    }
}
