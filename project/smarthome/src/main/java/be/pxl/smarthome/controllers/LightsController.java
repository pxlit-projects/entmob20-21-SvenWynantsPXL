package be.pxl.smarthome.controllers;

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

    private final GroupService groupService;
    private final LightService lightService;

    public LightsController(LightService lightService, GroupService groupService) {
        this.lightService = lightService;
        this.groupService = groupService;
    }

    @PostMapping(value = "/light")
    @Secured({"ROLE_ADMIN"})
    public Light addLight(@Valid @RequestBody LightDto lightDto) {
        return lightService.addLight(lightDto);
    }

    @PutMapping(value = "/{id}/flipSwitch")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public Light flipSwitch(@PathVariable int id) {
        Light light = lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        light = lightService.flipSwitch(light);
        LightGroup group = groupService.findLightGroupById(light.getGroup_id()).orElse(null);
        if (light.getOnState()) {
            if (group != null) {
                group.setHasOnState(true);
                groupService.updateGroup(group);
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
                groupService.updateGroup(group);
            }
        }

        return light;
    }

    @GetMapping(value = "/lights")
    public List<Light> getAllLights() {
        return lightService.getAllLights();
    }

    @GetMapping(value = "/light/{id}")
    public Light getLightById(@PathVariable int id) {
        return lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
    }

    @DeleteMapping(value = "light/{id}")
    @Secured({"ROLE_ADMIN"})
    public void removeLightById(@PathVariable int id) {
        Light light = lightService.findLightById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        lightService.removeLight(light);
    }
}
