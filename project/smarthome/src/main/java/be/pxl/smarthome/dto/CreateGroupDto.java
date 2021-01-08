package be.pxl.smarthome.dto;

import javax.validation.Valid;
import javax.validation.constraints.NotEmpty;

@Valid
public class CreateGroupDto {
    @NotEmpty
    public String Name;
}
