package be.pxl.smarthouse.models;

import java.util.List;

public class LightGroup {
    private String key;
    private String name;
    private List<String> lights;
    private String type;
    private Action action;

    public LightGroup(String key, String name, List<String> lights, String type, Action action) {
        this.key = key;
        this.name = name;
        this.lights = lights;
        this.type = type;
        this.action = action;
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

    public List<String> getLights() {
        return lights;
    }

    public void setLights(List<String> lights) {
        this.lights = lights;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Action getAction() {
        return action;
    }

    public void setAction(Action action) {
        this.action = action;
    }
}
