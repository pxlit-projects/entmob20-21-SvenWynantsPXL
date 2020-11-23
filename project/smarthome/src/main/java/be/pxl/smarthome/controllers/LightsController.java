package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(path = "/lights")
public class LightsController {

    @Autowired
    private LightService service;

    @PostMapping(value = "/light")
    @Secured({"ROLE_ADMIN"})
    public ResponseEntity addLight(@RequestBody LightDto lightDto) {
        try {
            service.addLight(lightDto);
        } catch (Exception e){
            e.printStackTrace();
            return new ResponseEntity(HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity(HttpStatus.OK);
    }

}
