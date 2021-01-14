package be.pxl.smarthome.service;

import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.dto.CreateGroupDto;
import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightGroup;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import javax.transaction.Transactional;
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
    public Optional<LightGroup> findLightGroupById(Integer id) {
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
        if (group == null){
            light.setGroup_id(null);
            lightDao.save(light);
        } else {
            List<Light> lights = group.getLights();
            if (lights == null || lights.size() == 0){
                lights = new ArrayList<>();
            }
            lights.add(light);
            group.setLights(lights);
            dao.save(group);
            return group;
        }
        return null;
    }

    @Override
    public LightGroup addGroup(CreateGroupDto createGroupDto) {
        LightGroup group = new LightGroup();
        group.setLights(new ArrayList<>());
        group.setHasOnState(false);
        group.setAllOnState(false);
        group.setBrightness(100);
        group.setName(createGroupDto.Name);
        group = dao.save(group);
        return group;
    }

    @Override
    @Transactional
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
    @Transactional
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

    @PostConstruct
    public void seedGroups() {
        CreateGroupDto dto1 = new CreateGroupDto();
        dto1.Name = "Living Room";
        boolean exists1 = false;
        CreateGroupDto dto2 = new CreateGroupDto();
        dto2.Name = "Kitchen";
        boolean exists2 = false;
        CreateGroupDto dto3 = new CreateGroupDto();
        dto3.Name = "Bedroom 1";
        boolean exists3 = false;
        List<LightGroup> groups = this.getAllGroups();
        for (LightGroup g : groups) {
            if (g.getName().equals(dto1.Name)) {
                exists1 = true;
            } else if (g.getName().equals(dto2.Name)) {
                exists2 = true;
            } else if (g.getName().equals(dto3.Name)) {
                exists3 = true;
            }
        }

        if (!exists1) {
            addGroup(dto1);
        }
        if (!exists2) {
            addGroup(dto2);
        }
        if (!exists3) {
            addGroup(dto3);
        }
    }
}
