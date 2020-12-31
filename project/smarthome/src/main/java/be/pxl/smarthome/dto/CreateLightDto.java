package be.pxl.smarthome.dto;

import be.pxl.smarthome.models.LightManufacturer;

import javax.validation.Valid;
import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.NotNull;

@Valid
public class LightDto {
    @NotEmpty
    public String Name;
    @NotEmpty
    public String Type;
    @NotNull
    public LightManufacturer LightManufacturer;
    public Integer LightGroupId;
}
