package be.pxl.smarthome.models;
import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "lightgroups")
public class LightGroup {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    private String name;
    @OneToMany
    private List<Light> lights;
    private boolean hasOnState;
    private boolean allOnState;
    private int brightness;

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

    public void setLights(List<Light> lightIds) {
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
}
