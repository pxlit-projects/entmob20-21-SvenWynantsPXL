package be.pxl.smarthome.dto;

import javax.validation.Valid;
import javax.validation.constraints.NotEmpty;

@Valid
public class GroupDto {
    @NotEmpty
    public String name;
}
