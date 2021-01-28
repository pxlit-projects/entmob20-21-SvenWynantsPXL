package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.CreateLightDto;
import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping(path = "lights")
public class LightsController {

    @Autowired
    private JmsTemplate jmsTemplate;

    @Autowired
    private GroupService _groupService;
    @Autowired
    private LightService _lightService;

    @PostMapping(value = "/light")
    @Secured({"ROLE_ADMIN"})
    public Light addLight(@Valid @RequestBody CreateLightDto createLightDto) {
        jmsTemplate.convertAndSend("lightListener", "light with name: " + createLightDto.Name + " added");
        return _lightService.addLight(createLightDto);
    }

    @PutMapping(value = "/{id}/flipSwitch")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public LightDto flipSwitch(@PathVariable int id) {
        jmsTemplate.convertAndSend("lightListener", "light with id: " + id + " requesting.");
        Light light = _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        light = _lightService.flipSwitch(light);
        if (light.getGroup_id() != null) {
            LightGroup group = _groupService.findLightGroupById(light.getGroup_id()).orElse(null);
            if (light.getOnState()) {
                if (group != null) {
                    group.setHasOnState(true);
                    _groupService.updateGroup(group);
                }
            } else {
                if (group != null) {
                    boolean hasOn = false;
                    for (Light l : group.getLights()) {
                        if (l.getOnState()) {
                            hasOn = true;
                        }
                    }
                    group.setHasOnState(hasOn);
                    _groupService.updateGroup(group);
                }
            }
        }

        return light.toDto();
    }

    @GetMapping(value = "/lights")
    public List<LightDto> getAllLights() {
        jmsTemplate.convertAndSend("lightListener", "All lights are requested from controller");
        return _lightService.getAllLights().stream().map(Light::toDto).collect(Collectors.toList());
    }

    @GetMapping(value = "/light/{id}")
    public LightDto getLightById(@PathVariable int id) {
        jmsTemplate.convertAndSend("lightListener", "Light with id: " + id + " requested");
        return _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id)).toDto();
    }

    @DeleteMapping(value = "/{id}")
    @Secured({"ROLE_ADMIN"})
    public void removeLightById(@PathVariable int id) {
        Light light = _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        jmsTemplate.convertAndSend("lightListener", "Light with id: " + id + " requested to delete");

        _lightService.removeLight(light);
    }

    @PutMapping(value = "/updateLight")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public LightDto updateLight(@RequestBody LightDto lightDto) {
        Light light = _lightService.findLightById(lightDto.Id)
                .orElseThrow(() -> new EntityNotFoundException(lightDto.Id));
        if (lightDto.OnTimer != null) {
            if (lightDto.OnTimer.equals("")) {
                light.setOnTimer(null);
            } else {
                DateTimeFormatter formatter;
                LocalDateTime timer;
                try {
                    formatter =  DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");
                    timer = LocalDateTime.parse(lightDto.OnTimer, formatter);
                } catch (DateTimeParseException e){
                    formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm");
                    timer = LocalDateTime.parse(lightDto.OnTimer, formatter);
                }
                light.setOnTimer(timer);
            }
        }
        light.setOnSunDown(lightDto.OnSunDown);
        light.setBrightness(lightDto.Brightness);
        light = _lightService.updateLight(light);
        jmsTemplate.convertAndSend("lightListener", "Light with id: " + lightDto.Id + " updated");
        return light.toDto();
    }
}
