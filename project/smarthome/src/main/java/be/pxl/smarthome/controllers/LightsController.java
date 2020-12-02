package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping(path = "lights")
public class LightsController {

    @Autowired
    private LightService service;

    @PostMapping(value = "/light")
    @Secured({"ADMIN"})
    public ResponseEntity addLight(@RequestBody LightDto lightDto) {
        try {
            service.addLight(lightDto);
        } catch (Exception e){
            e.printStackTrace();
            return new ResponseEntity(HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity(HttpStatus.OK);
    }

    @GetMapping(value = "/lights")
    public ResponseEntity getAllLights(){
        List<Light> lights = service.getAllLights();

        if (lights == null || lights.size() == 0) {
            return new ResponseEntity("No lights in the database", HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity(lights, HttpStatus.OK);
    }

    @GetMapping(value = "/light/{id}")
    public ResponseEntity getLightById(@PathVariable int id) {
        Light light = service.getLightById(id);
        if (light == null) {
            return new ResponseEntity("No light was found with this id", HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity(light, HttpStatus.OK);
    }

    @DeleteMapping(value = "light/{id}")
    public ResponseEntity removeLightById(@PathVariable int id) {
        Light light = service.getLightById(id);

        if (light == null) {
            return new ResponseEntity("Light with id wasn't found", HttpStatus.BAD_REQUEST);
        }

        service.removeLight(light);

        return new ResponseEntity("Light successfully removed", HttpStatus.OK);
    }
}
