package be.pxl.smarthome.dto;

import org.springframework.lang.NonNull;

public class LightDto {
    @NonNull
    public String name;
    @NonNull
    public String type;
    @NonNull
    public String manufacturerName;
    public int lightGroupId;
}
