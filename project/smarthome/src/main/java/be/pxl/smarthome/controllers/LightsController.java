package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.CreateLightDto;
import be.pxl.smarthome.dto.LightDto;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.*;
import javax.validation.Valid;
import java.util.List;

@RestController
@RequestMapping(path = "lights")
public class LightsController {

    private final GroupService _groupService;
    private final LightService _lightService;

    public LightsController(GroupService groupService, LightService lightService) {
        this._groupService = groupService;
        this._lightService = lightService;
    }

    @PostMapping(value = "/light")
    @Secured({"ROLE_ADMIN"})
    public Light addLight(@Valid @RequestBody CreateLightDto createLightDto) {
        return _lightService.addLight(createLightDto);
    }

    @PutMapping(value = "/{id}/flipSwitch")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public Light flipSwitch(@PathVariable int id) {
        Light light = _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        light = _lightService.flipSwitch(light);
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

        return light;
    }

    @GetMapping(value = "/lights")
    public List<Light> getAllLights() {
        return _lightService.getAllLights();
    }

    @GetMapping(value = "/light/{id}")
    public Light getLightById(@PathVariable int id) {
        return _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
    }

    @DeleteMapping(value = "light/{id}")
    @Secured({"ROLE_ADMIN"})
    public void removeLightById(@PathVariable int id) {
        Light light = _lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        _lightService.removeLight(light);
    }

    @PutMapping(value = "updateLight")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public Light updateLight(@RequestBody LightDto lightDto){
        Light light = _lightService.findLightById(lightDto.Id)
                .orElseThrow(() -> new EntityNotFoundException(lightDto.Id));
        light.setName(lightDto.Name);
        light.setBrightness(lightDto.Brightness);
        light = _lightService.updateLight(light);

        return light;
    }
}
