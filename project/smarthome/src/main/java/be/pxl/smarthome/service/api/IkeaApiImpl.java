package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.util.ArrayList;
import java.util.List;

public class IkeaApi {

    private HttpClient client;

    public IkeaApi(){
        client = HttpClient.newHttpClient();
    }

    public List<Light> getAllLights(){
        /*var request = HttpRequest.newBuilder(
                URI.create("https://ikeabridge/api/<username>/lights"))
                .build();

         */
        return new ArrayList<>();
    }
}
