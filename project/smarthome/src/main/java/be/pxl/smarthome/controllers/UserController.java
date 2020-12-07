package be.pxl.smarthome.controllers;

import be.pxl.smarthome.models.User;
import be.pxl.smarthome.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping(path = "users")
public class UserController {

    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    //Check if user is authorized or not before continuing to next screen
    @GetMapping(value = "login")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public ResponseEntity Login(){
        return new ResponseEntity(HttpStatus.OK);
    }

    @GetMapping(value = "getUsers")
    @Secured({"ROLE_ADMIN"})
    public List<User> getUsers(){
        return userService.getAllUsers();
    }
}
