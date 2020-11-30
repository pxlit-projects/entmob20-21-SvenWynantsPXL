package be.pxl.smarthome.dao;

import be.pxl.smarthome.models.LightGroup;
import org.springframework.data.jpa.repository.JpaRepository;

public interface LightGroupDao extends JpaRepository<LightGroup, Integer> {
}
