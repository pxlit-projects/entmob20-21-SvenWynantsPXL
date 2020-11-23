package be.pxl.smarthome.models;

import java.util.List;

public class State {
    private int bri;
    private boolean on;

    public State() {
    }

    public State(int bri, boolean on) {
        this.bri = bri;
        this.on = on;
    }

    public int getBri() {
        return bri;
    }

    public void setBri(int bri) {
        this.bri = bri;
    }

    public boolean isOn() {
        return on;
    }

    public void setOn(boolean on) {
        this.on = on;
    }
}
