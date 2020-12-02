package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.util.ArrayList;
import java.util.List;

@Service
public class PhilipsApiImpl implements PhilipsApi {
    private static Logger logger = LogManager.getLogger(PhilipsApiImpl.class);
    private HttpClient client;

    @PostConstruct
    public void createClient(){
        client = HttpClient.newHttpClient();
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
    }

    @Override
    public Light getLightByName(String name) {
        //TODO
        return null;
    }
}
