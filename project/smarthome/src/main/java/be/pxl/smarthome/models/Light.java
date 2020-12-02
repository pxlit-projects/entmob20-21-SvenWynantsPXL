package be.pxl.smarthome.models;

import javax.persistence.*;

@Entity
@Table(name = "lights")
public class Light {

    public Light() {
    }

    //to fill dummyApi
    public Light(int brightness, boolean isOnState, String type, String name, LightManufacturer manufacturer) {
        this.brightness = brightness;
        this.isOnState = isOnState;
        this.type = type;
        this.name = name;
        this.manufacturer = manufacturer;
    }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Integer id;
    @Column(name = "brightness")
    private int brightness;
    @Column(name = "ison")
    private boolean isOnState;
    @Column(name = "type")
    private String type;
    @Column(name = "name")
    private String name;
    @Column(name = "manufacturer")
    @Enumerated(EnumType.STRING)
    private LightManufacturer manufacturer;
    @ManyToOne
    @JoinColumn(name = "group_id", nullable = true)
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

    public boolean getOnState() {
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

    public LightManufacturer getManufacturer() {
        return manufacturer;
    }

    public void setManufacturer(LightManufacturer manufacturer) {
        this.manufacturer = manufacturer;
    }

    public LightGroup getGroup() {
        return group;
    }

    public void setGroup(LightGroup group) {
        this.group = group;
    }
}