package be.pxl.smarthome.service.api.impl;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.api.IkeaApi;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.net.http.HttpClient;
import java.util.ArrayList;
import java.util.List;

@Service
public class IkeaApiImpl implements IkeaApi {
    private static Logger logger = LogManager.getLogger(IkeaApiImpl.class);
    private HttpClient client;

    @PostConstruct
    public void createClient(){
        client = HttpClient.newHttpClient();
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
        //TODO: not yet implemented
        logger.info("a light was added to the ikea bridge");
        return;
    }

    @Override
    public void removeLight(String name) {
        //TODO
        logger.info("light was removed from the bridge");
    }

    @Override
    public Light getLightByName(String name) {
        //TODO
        return null;
    }
}
