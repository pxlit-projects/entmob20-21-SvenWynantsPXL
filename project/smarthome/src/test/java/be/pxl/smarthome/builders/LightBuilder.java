package be.pxl.smarthome.builders;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.models.LightManufacturer;

public class LightBuilder {
    private Light light;

    public LightBuilder(){
        light = new Light(100, false, "test", "test light", LightManufacturer.DUMMY);
    }

    public Light Build(){
        return light;
    }
}
