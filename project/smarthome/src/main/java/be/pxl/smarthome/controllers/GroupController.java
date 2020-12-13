package be.pxl.smarthome.controllers;

import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(path = "groups")
public class GroupController {

    private final GroupService groupService;
    private final LightService lightService;

    public GroupController(GroupService gService, LightService lService) {
        this.groupService = gService;
        this.lightService = lService;
    }

    @GetMapping(value = "/{id}")
    public LightGroup getGroup(@PathVariable int id) {
        return groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
    }

    @DeleteMapping(value = "/{id}")
    public void removeGroupById(@PathVariable int id) {
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));

        groupService.removeGroup(group);
    }

    @PostMapping(value = "/{groupId}/addLight/{lightId}")
    public LightGroup addLightToGroup(@PathVariable int groupId, @PathVariable int lightId){
        Light light = lightService.findLightById(lightId)
                .orElseThrow(() -> new EntityNotFoundException(lightId));
        LightGroup group = groupService.findLightGroupById(groupId)
                .orElseThrow(() -> new EntityNotFoundException(groupId));

        group = groupService.addLightToGroup(group, light);

        return group;
    }
}
