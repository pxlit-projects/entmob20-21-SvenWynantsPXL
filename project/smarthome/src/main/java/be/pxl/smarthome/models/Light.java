package be.pxl.smarthome.models;

import javax.persistence.*;

@Entity
public class Light {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    private State state;
    private String type;
    private String name;
    private String manufacturerName;
    @ManyToOne
    private LightGroup group;

    public int getId() {
        return id;
    }

    public State getState() {
        return state;
    }

    public void setState(State state) {
        this.state = state;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getManufacturerName() {
        return manufacturerName;
    }

    public void setManufacturerName(String manufacturerName) {
        this.manufacturerName = manufacturerName;
    }

    public LightGroup getGroup() {
        return group;
    }

    public void setGroup(LightGroup group) {
        this.group = group;
    }
}