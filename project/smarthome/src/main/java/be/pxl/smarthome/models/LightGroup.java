package be.pxl.smarthome.models;

import java.util.ArrayList;
import java.util.List;

public class LightGroup {
    private String key;
    private String name;
    private List<Integer> lightIds;
    private State state;
    private String type;

    public LightGroup(String key, String name, String type) {
        this.key = key;
        this.name = name;
        this.lightIds = new ArrayList<Integer>();
        this.type = type;
    }

    public LightGroup() {
    }

    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Integer> getLights() { return lightIds; }

    public void setLights(List<Integer> lightIds) {
        this.lightIds = lightIds;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public State getState() {
        return state;
    }

    public void setState(State state) {
        this.state = state;
    }
}
