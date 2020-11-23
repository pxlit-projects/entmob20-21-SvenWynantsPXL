package be.pxl.smarthome.service;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dao.impl.DummyLightDaoImpl;
import be.pxl.smarthome.dao.impl.IkeaLightDaoImpl;
import be.pxl.smarthome.dao.impl.PhilipsLightDaoImpl;
import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.State;
import org.springframework.stereotype.Service;

@Service
public class LightService {
    private LightDao dao;

    public Light addLight(LightDto lightDto) {
        Light light = new Light();

        if (lightDto.manufacturerName.equals("Philips")){
            dao = new PhilipsLightDaoImpl();
        } else if (lightDto.manufacturerName.equals("Dummy")) {
            dao = new DummyLightDaoImpl();
        } else if (lightDto.manufacturerName.equals("Ikea")){
            dao = new IkeaLightDaoImpl();
        } else {
            return null;
        }

        light.setName(lightDto.name);
        light.setManufacturerName(lightDto.manufacturerName);
        light.setState(new State());
        light.setType(lightDto.type);
        light = dao.addLight(light);

        return light;
    }
}
