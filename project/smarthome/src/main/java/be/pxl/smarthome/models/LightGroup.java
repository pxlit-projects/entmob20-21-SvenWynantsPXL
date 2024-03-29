package be.pxl.smarthome.models;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "lightgroups")
public class LightGroup {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private int id;
    @Column(name = "name")
    private String name;
    @OneToMany(fetch = FetchType.EAGER, cascade = CascadeType.PERSIST)
    @JoinColumn(name = "group_id")
    private List<Light> lights;
    @Column(name = "hason")
    private boolean hasOnState;
    @Column(name = "allon")
    private boolean allOnState;
    @Column(name = "brightness")
    private int brightness;
    @ManyToMany
    @JsonIgnore
    private List<User> users;

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Light> getLights() { return lights; }

    public void setLights(List<Light> lights) {
        this.lights = lights;
    }

    public boolean isHasOnState() {
        return hasOnState;
    }

    public void setHasOnState(boolean hasOnState) {
        this.hasOnState = hasOnState;
    }

    public boolean isAllOnState() {
        return allOnState;
    }

    public void setAllOnState(boolean allOnState) {
        this.allOnState = allOnState;
    }

    public int getBrightness() {
        return brightness;
    }

    public void setBrightness(int brightness) {
        this.brightness = brightness;
    }

    public List<User> getUsers() {
        return users;
    }

    public void setUsers(List<User> users) {
        this.users = users;
    }
}
