package be.pxl.smarthome.models;

import be.pxl.smarthome.dto.LightDto;
import com.fasterxml.jackson.annotation.JsonIgnore;
import org.apache.tomcat.jni.Local;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

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
        this.setOnSunDown(false);
        this.setOnTimer(null);
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
    @Column(name = "group_id", nullable = true)
    private Integer group_id;
    @Column(name = "ontimer")
    private LocalDateTime onTimer;
    @Column(name = "onsundown")
    private boolean onSunDown;

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

    public Integer getGroup_id(){
        return this.group_id;
    }

    public LocalDateTime getOnTimer() {
        return onTimer;
    }

    public void setOnTimer(LocalDateTime onTimer) {
        this.onTimer = onTimer;
    }

    public boolean isOnSunDown() {
        return onSunDown;
    }

    public void setOnSunDown(boolean onSunDown) {
        this.onSunDown = onSunDown;
    }

    public LightDto toDto(){
        LightDto dto = new LightDto();
        dto.Id = this.id;
        dto.Name = this.name;
        dto.Brightness = this.brightness;
        dto.Manufacturer = this.manufacturer;
        dto.OnState = this.isOnState;
        dto.Type = this.type;
        if (this.onTimer != null){
            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm");
            dto.OnTimer = this.onTimer.format(formatter);
        }else {
            dto.OnTimer = "";
        }
        dto.OnSunDown = this.onSunDown;
        if (this.group_id == null){
            dto.GroupId = 0;
        } else {
            dto.GroupId = this.group_id;
        }
        return dto;
    }
}