package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.ResponseUserDto;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.User;
import be.pxl.smarthome.service.UserService;
import org.springframework.security.access.annotation.Secured;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
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
    @GetMapping(value = "/login")
    @Secured({"ROLE_ADMIN", "ROLE_USER"})
    public ResponseUserDto Login(){
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        UserDetails details = (UserDetails) auth.getPrincipal();
        User user = userService.findUserByName(details.getUsername());
        return user.toUserDto();
    }

    @GetMapping(value = "getUsers")
    @Secured({"ROLE_ADMIN"})
    public List<ResponseUserDto> getUsers(){
        return userService.getAllUsers();
    }
}
