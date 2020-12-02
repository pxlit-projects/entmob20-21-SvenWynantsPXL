package be.pxl.smarthome.service.api.impl;

import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.api.DummyApi;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.util.ArrayList;
import java.util.List;

@Service
public class DummyApiImpl implements DummyApi {
    private List<Light> lights;
    private static final Logger logger = LogManager.getLogger(DummyApiImpl.class);
    @Autowired
    private LightGroupDao groupDao;

    @PostConstruct
    public void seedData() {
        lights = new ArrayList<>();
        Light light1 = new Light(100, false, "dummy light", "Dummy light 1", "Dummy");
        light1.setGroup(groupDao.findById(1).orElse(null));
        Light light2 = new Light(100, false, "dummy light", "Dummy light 2", "Dummy");
        light2.setGroup(groupDao.findById(1).orElse(null));
        Light light3 = new Light(100, false, "dummy light", "Dummy light 3", "Dummy");
        lights.add(light1);
        lights.add(light2);
        lights.add(light3);
    }

    public Light getLightByName(String name) {
        Light light = new Light();
        for (Light l : lights) {
            if (l.getName().equals(name)) {
                light = l;
            }
        }
        return light;
    }

    public List<Light> getAllLights(){
        logger.info("All lights from dummy database returned");
        return lights;
    }

    public void addLight(Light light) {
        lights.add(light);
        logger.info("Light added to dummy datalist");
    }

    @Override
    public void removeLight(String name) {
        Light toRemove = getLightByName(name);
        lights.remove(toRemove);
        logger.info("light removed from dummy api");
    }
}
