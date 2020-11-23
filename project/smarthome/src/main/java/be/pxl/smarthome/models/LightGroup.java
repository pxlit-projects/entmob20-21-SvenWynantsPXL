package be.pxl.smarthome.models;
import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
public class LightGroup {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    private String name;
    @OneToMany
    private List<Light> lights;
    private State state;
    private String type;

    public LightGroup(int id, String name, String type) {
        this.id = id;
        this.name = name;
        this.lights = new ArrayList<Light>();
        this.type = type;
        this.state = new State();
    }

    public LightGroup() {
    }

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

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public State getState() {
        return state;
    }

    public void setState(State state) {
        this.state = state;
    }
}
