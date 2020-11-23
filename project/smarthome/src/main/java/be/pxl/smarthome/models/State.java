package be.pxl.smarthome.models;

import java.util.List;

public class State {
    private int bri;
    private boolean hasOn;
    private boolean allOn;

    public State() {
        this.bri = 0;
        this.hasOn = false;
        this.allOn = false;
    }

    public State(int bri, boolean hasOn, boolean allOn) {
        this.bri = bri;
        this.hasOn = hasOn;
        this.allOn = allOn;
    }

    public int getBri() {
        return bri;
    }

    public void setBri(int bri) {
        this.bri = bri;
    }

    public boolean isHasOn() {
        return hasOn;
    }

    public void setHasOn(boolean hasOn) {
        this.hasOn = hasOn;
    }

    public boolean isAllOn() {
        return allOn;
    }

    public void setAllOn(boolean allOn) {
        this.allOn = allOn;
    }
}
