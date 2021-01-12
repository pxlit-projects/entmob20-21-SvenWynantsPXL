package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.CreateGroupDto;
import be.pxl.smarthome.exceptions.EntityAlreadyExistsException;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(path = "groups")
public class GroupController {

    @Autowired
    private GroupService groupService;
    @Autowired
    private LightService lightService;

    @GetMapping(value = "/{id}")
    public LightGroup getGroup(@PathVariable int id) {
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
        return group;
    }

    @DeleteMapping(value = "/{id}")
    public void removeGroupById(@PathVariable int id) {
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
        groupService.removeGroup(group);
    }

    @PostMapping(value = "/{groupId}/addLight/{lightId}")
    public void addLightToGroup(@PathVariable int groupId, @PathVariable int lightId) {
        Light light = lightService.findLightById(lightId)
                .orElseThrow(() -> new EntityNotFoundException(lightId));
        LightGroup group = groupService.findLightGroupById(groupId)
                .orElse(null);
        groupService.addLightToGroup(group, light);
    }

    @PostMapping(value = "/addGroup")
    public LightGroup addGroup(@RequestBody CreateGroupDto createGroupDto) {
        List<LightGroup> groups = groupService.getAllGroups();

        if (groups.stream().anyMatch(l -> l.getName().equals(createGroupDto.Name))){
            throw new EntityAlreadyExistsException(createGroupDto.Name);
        }

        LightGroup group = groupService.addGroup(createGroupDto);
        return group;
    }

    @PutMapping(value = "/{id}/turnAllOn")
    public LightGroup turnAllLightsOn(@PathVariable int id) {
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
        group = groupService.turnOnLights(group);
        return group;
    }

    @PutMapping(value = "/{id}/turnAllOff")
    public LightGroup turnAllLightsOff(@PathVariable int id) {
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
        group = groupService.turnOffLights(group);
        return group;
    }

    @GetMapping(value = "/groups")
    public List<LightGroup> getAllGroups() {
        return groupService.getAllGroups();
    }
}
