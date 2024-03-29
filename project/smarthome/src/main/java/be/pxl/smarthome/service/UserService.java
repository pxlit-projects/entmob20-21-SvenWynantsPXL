package be.pxl.smarthome.service;

import be.pxl.smarthome.dto.ResponseUserDto;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.models.User;

import java.util.List;
import java.util.Optional;

public interface UserService {
    List<ResponseUserDto> getAllUsers();

    Optional<User> findUserById(int id);

    User findUserByName(String username);

    void restrictUser(User user, LightGroup group);

    void removeRestriction(User user, LightGroup group);
}
