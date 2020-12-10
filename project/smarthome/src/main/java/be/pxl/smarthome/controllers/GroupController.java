package be.pxl.smarthome.controllers;

import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(path = "groups")
public class GroupController {

    private final GroupService service;

    public GroupController(GroupService service) {
        this.service = service;
    }

    @GetMapping(value = "/{id}")
    public LightGroup getGroup(@PathVariable int id) {
        return service.getLightGroup(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
    }

    @DeleteMapping(value = "/{id}")
    public void removeGroupById(@PathVariable int id) {
        LightGroup group = service.getLightGroup(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        service.removeGroup(group);
    }
}
