package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.GroupDto;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

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
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
        List<Light> lights = lightService.getAllLights().stream().filter(l -> l.getGroup() != null && l.getGroup().getId() == group.getId()).collect(Collectors.toList());
        for (Light light : lights){
            light.setGroup(null);
        }
        group.setLights(lights);
        return group;
    }

    @DeleteMapping(value = "/{id}")
    public void removeGroupById(@PathVariable int id) {
        LightGroup group = groupService.findLightGroupById(id)
                .orElseThrow(() -> new EntityNotFoundException(id));
        List<Light> lights = lightService.getAllLights().stream().filter(l -> l.getGroup() != null && l.getGroup().getId() == group.getId()).collect(Collectors.toList());
        for (Light light : lights) {
            lightService.removeGroup(light);
        }
        groupService.removeGroup(group);
    }

    @PostMapping(value = "/{groupId}/addLight/{lightId}")
    public LightGroup addLightToGroup(@PathVariable int groupId, @PathVariable int lightId) {
        Light light = lightService.findLightById(lightId)
                .orElseThrow(() -> new EntityNotFoundException(lightId));
        LightGroup group = groupService.findLightGroupById(groupId)
                .orElseThrow(() -> new EntityNotFoundException(groupId));

        group = groupService.addLightToGroup(group, light);

        return group;
    }

    @PostMapping(value = "/addGroup")
    public LightGroup addGroup(@RequestBody GroupDto groupDto) {
        LightGroup group = groupService.addGroup(groupDto);
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
