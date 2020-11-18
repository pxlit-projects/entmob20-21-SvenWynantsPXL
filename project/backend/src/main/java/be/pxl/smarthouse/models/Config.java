package be.pxl.smarthouse.models;

public class Config {
    private String archetype;
    private String function;
    private String direction;

    public Config(String archetype, String function, String direction) {
        this.archetype = archetype;
        this.function = function;
        this.direction = direction;
    }

    public Config() {
    }

    public String getArchetype() {
        return archetype;
    }

    public void setArchetype(String archetype) {
        this.archetype = archetype;
    }

    public String getFunction() {
        return function;
    }

    public void setFunction(String function) {
        this.function = function;
    }

    public String getDirection() {
        return direction;
    }

    public void setDirection(String direction) {
        this.direction = direction;
    }
}
