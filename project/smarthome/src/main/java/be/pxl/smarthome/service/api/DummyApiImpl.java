package be.pxl.smarthome.service.api;

import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.models.Light;
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
    private static Logger logger = LogManager.getLogger(DummyApiImpl.class);
    @Autowired
    private LightGroupDao groupDao;

    @PostConstruct
    public void seedData() {
        lights = new ArrayList<>();
        Light light1 = new Light(100, false, "dummy light", "Dummy light 1", "Dummy");
        //light1.setGroup(groupDao.findById(1).orElse(null));
        Light light2 = new Light(100, false, "dummy light", "Dummy light 2", "Dummy");
        //light2.setGroup(groupDao.findById(1).orElse(null));
        Light light3 = new Light(100, false, "dummy light", "Dummy light 3", "Dummy");
        lights.add(light1);
        lights.add(light2);
        lights.add(light3);
    }

    public List<Light> getAllLights(){
        logger.debug("All lights from dummy database returned");
        return lights;
    }

    public void addLight(Light light) {
        lights.add(light);
        logger.debug("Light added to dummy datalist");
    }
}
