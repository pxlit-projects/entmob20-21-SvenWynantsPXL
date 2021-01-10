package be.pxl.smarthome.controllers;

import be.pxl.smarthome.dto.ResponseUserDto;
import be.pxl.smarthome.exceptions.EntityNotFoundException;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.models.User;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.annotation.Secured;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(path = "users")
public class UserController {

    @Autowired
    private UserService userService;
    @Autowired
    private GroupService groupService;

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

    @PutMapping(value = "/{userId}/addRestriction/{groupId}")
    @Secured({"ROLE_ADMIN"})
    public void restrictUserToGroup(@PathVariable int userId, @PathVariable int groupId){
        User user = userService.findUserById(userId)
                .orElseThrow(() -> new EntityNotFoundException(userId));
        LightGroup group = groupService.findLightGroupById(groupId)
                .orElseThrow(() -> new EntityNotFoundException(groupId));

        userService.restrictUser(user, group);
    }

    @PutMapping(value = "/{userId}/removeRestriction/{groupId}")
    @Secured({"ROLE_ADMIN"})
    public void removeRestrictionForUser(@PathVariable int userId, @PathVariable int groupId){
        User user = userService.findUserById(userId)
                .orElseThrow(() -> new EntityNotFoundException(userId));
        LightGroup group = groupService.findLightGroupById(groupId)
                .orElseThrow(() -> new EntityNotFoundException(groupId));

        userService.removeRestriction(user, group);
    }
}
