package be.pxl.smarthome.service.impl;

import be.pxl.smarthome.dao.UserDao;
import be.pxl.smarthome.dto.ResponseUserDto;
import be.pxl.smarthome.models.User;
import be.pxl.smarthome.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class UserServiceImpl implements UserService {

    @Autowired
    private UserDao dao;

    @Override
    public List<ResponseUserDto> getAllUsers() {
        List<ResponseUserDto> users = dao.findAll().stream()
                .map(User::toUserDto).collect(Collectors.toList());

        return users;
    }

    @Override
    public Optional<User> findUserById(int id) {
        return Optional.ofNullable(id)
                // if id is null => wont execute
                .flatMap(dao::findById);
    }

    @PostConstruct
    public void seedData(){
        List<User> users = new ArrayList<>();

        User user1 = new User(1, "sven", "$2a$10$6YXbH2u2DCqA.fT3gkmlBe82/aTgTiWutYUwsMrSLdAehXhba67BK", true, "ROLE_ADMIN");
        User user2 = new User(2, "michiel", "$2a$10$6YXbH2u2DCqA.fT3gkmlBe82/aTgTiWutYUwsMrSLdAehXhba67BK", true, "ROLE_USER");
        User user3 = new User(3, "jens", "$2a$10$6YXbH2u2DCqA.fT3gkmlBe82/aTgTiWutYUwsMrSLdAehXhba67BK", true, "ROLE_USER");
        users.add(user1);
        users.add(user2);
        users.add(user3);

        for (User user : users) {
            if (dao.findById(user.getId()).isEmpty()){
                dao.save(user);
            }
        }
    }
}
