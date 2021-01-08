package be.pxl.smarthome.models;

import be.pxl.smarthome.dto.ResponseUserDto;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "users")
public class User {
    @Id
    @Column(name = "id")
    private int id;
    @Column(name = "name")
    private String name;
    @Column(name = "password")
    private String password;
    @Column(name = "enabled")
    private boolean enabled;
    @Column(name = "role")
    private String role;
    @ManyToMany
    private List<LightGroup> groups;

    public User() {
    }

    public User(int id, String name, String password, boolean enabled, String role) {
        this.id = id;
        this.name = name;
        this.password = password;
        this.enabled = enabled;
        this.role = role;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public boolean isEnabled() {
        return enabled;
    }

    public void setEnabled(boolean enabled) {
        this.enabled = enabled;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }

    public ResponseUserDto toUserDto() {
        ResponseUserDto dto = new ResponseUserDto();
        dto.id = this.id;
        dto.name = this.name;
        dto.role = this.role;
        return dto;
    }
}
