package be.pxl.smarthome.dto;

import be.pxl.smarthome.models.LightManufacturer;

public class LightDto {
    public int Id;
    public String Name;
    public int Brightness;
    public String Type;
    public boolean OnState;
    public LightManufacturer Manufacturer;
    public int GroupId;
}
