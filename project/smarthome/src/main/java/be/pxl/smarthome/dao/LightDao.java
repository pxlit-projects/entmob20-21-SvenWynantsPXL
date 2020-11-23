package be.pxl.smarthome.dao;

import be.pxl.smarthome.models.Light;

import java.util.List;

public interface LightDao {
    List<Light> getAllLights();
}
