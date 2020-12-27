package be.pxl.smarthome.service.api.impl;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightManufacturer;
import be.pxl.smarthome.service.api.LightApi;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.net.http.HttpClient;
import java.util.ArrayList;
import java.util.List;

@Service
public class IkeaApiImpl implements LightApi {
    private static Logger logger = LogManager.getLogger(IkeaApiImpl.class);
    private HttpClient client;

    @PostConstruct
    public void createClient(){
        client = HttpClient.newHttpClient();
    }

    @Override
    public LightManufacturer getManufacturer() {
        return LightManufacturer.IKEA;
    }

    public List<Light> getAllLights(){
        /*var request = HttpRequest.newBuilder(
                URI.create("https://ikeabridge/api/<username>/lights"))
                .build();
         */
        logger.info("All lights from ikea requested");
        return new ArrayList<>();
    }

    @Override
    public void addLight(Light light) {
        logger.info("a light was added to the ikea bridge");
        return;
    }

    @Override
    public void removeLight(String name) {
        logger.info("light was removed from the bridge");
    }

    @Override
    public Light getLightByName(String name) {
        logger.info("light from ikea api requested");
        return null;
    }

    @Override
    public void changeState(Light light) {
        logger.info("State of ikea light changed to" + light.getOnState());
    }

    @Override
    public void turnOnLight(Light light) {
        logger.info("Ikea light turned on");
    }

    @Override
    public void turnOffLight(Light light) {
        logger.info("Ikea light turned off");
    }

    @Override
    public void removeGroup(Light light) {
        logger.info("Group removed from Ikea light with id " + light.getId());
    }
}
