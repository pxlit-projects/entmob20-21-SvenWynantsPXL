package be.pxl.smarthome.dto;

import be.pxl.smarthome.models.LightGroup;

import java.util.List;

public class ResponseUserDto {
    public int id;
    public String name;
    public String role;
    public List<LightGroup> groups;
}
