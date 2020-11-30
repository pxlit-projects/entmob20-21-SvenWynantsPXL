package be.pxl.smarthome.models;

import javax.persistence.*;

@Entity
@Table(name = "lights")
public class Light {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    private int brightness;
    private boolean isOnState;
    private String type;
    private String name;
    private String manufacturerName;
    @ManyToOne
    private LightGroup group;

    public Integer getId() {
        return id;
    }

    public int getBrightness() {
        return brightness;
    }

    public void setBrightness(int brightness) {
        this.brightness = brightness;
    }

    public boolean isOnState() {
        return isOnState;
    }

    public void setOnState(boolean onState) {
        isOnState = onState;
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