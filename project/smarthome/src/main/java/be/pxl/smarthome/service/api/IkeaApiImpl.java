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
        logger.debug("All lights from ikea requested");
        return new ArrayList<>();
    }

    @Override
    public void addLight(Light light) {
        //TODO: not yet implemented
        logger.debug("a light was added to the ikea bridge");
        return;
    }
}
