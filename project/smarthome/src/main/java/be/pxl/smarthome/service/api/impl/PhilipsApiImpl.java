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
public class PhilipsApiImpl implements LightApi {
    private static Logger logger = LogManager.getLogger(PhilipsApiImpl.class);
    private HttpClient client;

    @PostConstruct
    public void createClient(){
        client = HttpClient.newHttpClient();
    }

    @Override
    public LightManufacturer getManufacturer() {
        return LightManufacturer.PHILIPS;
    }

    public List<Light> getAllLights(){
        /*  create request when there is a bridge to call
        HttpClient client = HttpClient.newHttpClient();
        var request = HttpRequest.newBuilder(
                URI.create("https://philipsbridge/api/<username>/lights")
        );
        */
        logger.info("All lights from philips bridge requested");
        return new ArrayList<>();
    }

    @Override
    public void addLight(Light light) {
        //TODO: not yet implemented
        logger.info("A light was added to the philips bridge");
        return;
    }

    @Override
    public void removeLight(String name) {
        //TODO
        logger.info("Light removed from philips api.");
    }

    @Override
    public Light getLightByName(String name) {
        //TODO
        logger.info("Light requested from philips api");
        return null;
    }

    @Override
    public void changeState(Light light) {
        logger.info("State of philips light changed to " + light.getOnState());
        //TODO
    }
}
