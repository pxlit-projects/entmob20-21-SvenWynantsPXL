package be.pxl.smarthome.dao;

import be.pxl.smarthome.models.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserDao extends JpaRepository<User, Integer> {

}
