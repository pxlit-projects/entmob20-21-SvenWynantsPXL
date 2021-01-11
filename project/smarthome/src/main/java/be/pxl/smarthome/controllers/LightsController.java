package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.CreateLightDto;
import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping(path = "lights")
public class LightsController {

    @Autowired
    private GroupService _groupService;
    @Autowired
    private LightService _lightService;

    @PostMapping(value = "/light")
    @Secured({"ROLE_ADMIN"})
    public Light addLight(@Valid @RequestBody CreateLightDto createLightDto) {
        return _lightService.addLight(createLightDto);
    }

    @PutMapping(value = "/{id}/flipSwitch")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public LightDto flipSwitch(@PathVariable int id) {
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
        return _lightService.getAllLights().stream().map(Light::toDto).collect(Collectors.toList());
    }

    @GetMapping(value = "/light/{id}")
    public LightDto getLightById(@PathVariable int id) {
        return _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id)).toDto();
    }

    @DeleteMapping(value = "/{id}")
    @Secured({"ROLE_ADMIN"})
    public void removeLightById(@PathVariable int id) {
        Light light = _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

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
                DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");
                light.setOnTimer(LocalDateTime.parse(lightDto.OnTimer, formatter));
            }
        }
        light.setBrightness(lightDto.Brightness);
        light = _lightService.updateLight(light);

        return light.toDto();
    }
}
