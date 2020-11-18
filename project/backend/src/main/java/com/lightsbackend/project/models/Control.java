package com.lightsbackend.project.models;

import java.util.List;

public class Control {
    private int mindimlevel;
    private int maxlumen;
    private String colorgamuttype;
    private List<List<Float>> colorgamut;
    private Ct ct;

    public Control(int mindimlevel, int maxlumen, String colorgamuttype, List<List<Float>> colorgamut, Ct ct) {
        this.mindimlevel = mindimlevel;
        this.maxlumen = maxlumen;
        this.colorgamuttype = colorgamuttype;
        this.colorgamut = colorgamut;
        this.ct = ct;
    }

    public Control() {
    }

    public int getMindimlevel() {
        return mindimlevel;
    }

    public void setMindimlevel(int mindimlevel) {
        this.mindimlevel = mindimlevel;
    }

    public int getMaxlumen() {
        return maxlumen;
    }

    public void setMaxlumen(int maxlumen) {
        this.maxlumen = maxlumen;
    }

    public String getColorgamuttype() {
        return colorgamuttype;
    }

    public void setColorgamuttype(String colorgamuttype) {
        this.colorgamuttype = colorgamuttype;
    }

    public List<List<Float>> getColorgamut() {
        return colorgamut;
    }

    public void setColorgamut(List<List<Float>> colorgamut) {
        this.colorgamut = colorgamut;
    }

    public Ct getCt() {
        return ct;
    }

    public void setCt(Ct ct) {
        this.ct = ct;
    }
}
