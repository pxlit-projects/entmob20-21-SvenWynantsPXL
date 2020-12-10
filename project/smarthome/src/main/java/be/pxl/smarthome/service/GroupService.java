package be.pxl.smarthome.service;

import be.pxl.smarthome.models.LightGroup;

import java.util.List;
import java.util.Optional;

public interface GroupService {
    Optional<LightGroup> getLightGroup(int id);
    List<LightGroup> getAllGroups();
    void removeGroup(LightGroup group);
    LightGroup updateGroup(LightGroup group);
}
