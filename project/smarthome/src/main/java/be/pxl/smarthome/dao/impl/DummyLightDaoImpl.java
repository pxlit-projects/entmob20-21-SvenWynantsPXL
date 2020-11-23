package be.pxl.smarthome.dao.impl;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.models.Light;

import java.util.List;

public class DummyLightDaoImpl implements LightDao {

    public DummyLightDaoImpl(){

    }

    @Override
    public List<Light> getAllLights() {
        return null;
    }

    @Override
    public Light addLight(Light light) {
        return null;
    }
}
