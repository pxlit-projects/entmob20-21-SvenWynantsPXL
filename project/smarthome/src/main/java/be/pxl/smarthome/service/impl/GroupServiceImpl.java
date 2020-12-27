package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.dto.GroupDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.api.impl.DummyApiImpl;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class GroupServiceImpl implements GroupService {
    private static final Logger logger = LogManager.getLogger(GroupServiceImpl.class);
    @Autowired
    private LightGroupDao dao;
    @Autowired
    private LightDao lightDao;
    @Autowired
    private LightApiService apiService;

    @Override
    public Optional<LightGroup> findLightGroupById(int id) {
        return Optional.ofNullable(id)
                .flatMap(dao::findById);
    }

    public List<LightGroup> getAllGroups() {
        return dao.findAll();
    }

    @Override
    public void removeGroup(LightGroup group) {
        dao.delete(group);
    }

    @Override
    public LightGroup updateGroup(LightGroup group) {
        return dao.save(group);
    }

    public LightGroup addLightToGroup(LightGroup group, Light light) {
        List<Light> lights = group.getLights();
        lights.add(light);
        group.setLights(lights);
        dao.save(group);
        return group;
    }

    @Override
    public LightGroup addGroup(GroupDto groupDto) {
        LightGroup group = new LightGroup();
        group.setLights(new ArrayList<>());
        group.setHasOnState(false);
        group.setAllOnState(false);
        group.setBrightness(100);
        group.setName(groupDto.name);
        group = dao.save(group);
        return group;
    }

    @Override
    public LightGroup turnOnLights(LightGroup group) {
        group.setAllOnState(true);
        for (Light light : group.getLights()) {
            apiService.turnOnLight(light);
            light.setOnState(true);
            lightDao.save(light);
        }
        group.setHasOnState(true);
        group = dao.save(group);
        logger.info("All lights in group with ID " + group.getId() + " are on.");
        return group;
    }

    @Override
    public LightGroup turnOffLights(LightGroup group) {
        group.setAllOnState(false);
        group.setHasOnState(false);
        for (Light light : group.getLights()) {
            apiService.turnOffLight(light);
            light.setOnState(false);
            lightDao.save(light);
        }
        group = dao.save(group);
        logger.info("All lights in group with ID " + group.getId() + " are off.");
        return group;
    }
}
