package be.pxl.smarthouse.models;

import java.util.List;

public class State {
    private int bri;
    private boolean on;
    private int hue;
    private int sat;
    private List<Float> xy;
    private int ct;
    private String alert;
    private String effect;
    private String colormode;
    private boolean reachable;

    public State() {
    }

    public State(int bri, boolean on, int hue, int sat, List<Float> xy, int ct, String alert, String effect, String colormode, boolean reachable) {
        this.bri = bri;
        this.on = on;
        this.hue = hue;
        this.sat = sat;
        this.xy = xy;
        this.ct = ct;
        this.alert = alert;
        this.effect = effect;
        this.colormode = colormode;
        this.reachable = reachable;
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

    public int getHue() {
        return hue;
    }

    public void setHue(int hue) {
        this.hue = hue;
    }

    public int getSat() {
        return sat;
    }

    public void setSat(int sat) {
        this.sat = sat;
    }

    public List<Float> getXy() {
        return xy;
    }

    public void setXy(List<Float> xy) {
        this.xy = xy;
    }

    public int getCt() {
        return ct;
    }

    public void setCt(int ct) {
        this.ct = ct;
    }

    public String getAlert() {
        return alert;
    }

    public void setAlert(String alert) {
        this.alert = alert;
    }

    public String getEffect() {
        return effect;
    }

    public void setEffect(String effect) {
        this.effect = effect;
    }

    public String getColormode() {
        return colormode;
    }

    public void setColormode(String colormode) {
        this.colormode = colormode;
    }

    public boolean isReachable() {
        return reachable;
    }

    public void setReachable(boolean reachable) {
        this.reachable = reachable;
    }
}
