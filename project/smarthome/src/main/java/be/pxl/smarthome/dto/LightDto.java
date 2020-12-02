package be.pxl.smarthome.dto;

import be.pxl.smarthome.models.LightManufacturer;

import javax.validation.Valid;
import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.NotNull;

@Valid
public class LightDto {
    @NotEmpty
    public String name;
    @NotEmpty
    public String type;
    @NotNull
    public LightManufacturer manufacturer;
    public Integer lightGroupId;
}
