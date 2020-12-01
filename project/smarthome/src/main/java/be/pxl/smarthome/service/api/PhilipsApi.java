package be.pxl.smarthome.service.api;

import be.pxl.smarthome.models.Light;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.util.ArrayList;
import java.util.List;

public class PhilipsApi {
    HttpClient client;

    public PhilipsApi(){
        client = HttpClient.newHttpClient();
    }

    public List<Light> getAllLights(){
        /*  create request when there is a bridge to call
        var request = HttpRequest.newBuilder(
                URI.create("https://philipsbridge/api/<username>/lights")
        );
        */
        return new ArrayList<>();
    }
}
