package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.Optional;

@Service
public class GroupServiceImpl implements GroupService {

    @Autowired
    private LightGroupDao dao;

    @Override
    public Optional<LightGroup> getLightGroup(int id) {
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
}
