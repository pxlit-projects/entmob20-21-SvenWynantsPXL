package be.pxl.smarthome.dto;

import javax.validation.Valid;
import javax.validation.constraints.NotEmpty;

@Valid
public class LightDto {
    @NotEmpty
    public String name;
    @NotEmpty
    public String type;
    @NotEmpty
    public String manufacturerName;
    public Integer lightGroupId;
}
