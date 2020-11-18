package com.lightsbackend.project.models;

import java.util.HashMap;
import java.util.Map;


public class Light {
    private State state;
    private Swupdate swupdate;
    private String type;
    private String name;
    private String modelid;
    private String manufacturername;
    private String productname;
    private Capabilities capabilities;
    private Config config;
    private String uniqueid;
    private String swversion;
    private String swconfigid;
    private String productid;

    private Map<String, Object> additionalProperties = new HashMap<String, Object>();

    public State getState() {
        return state;
    }

    public void setState(State state) {
        this.state = state;
    }

    public Swupdate getSwupdate() {
        return swupdate;
    }

    public void setSwupdate(Swupdate swupdate) {
        this.swupdate = swupdate;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getModelid() {
        return modelid;
    }

    public void setModelid(String modelid) {
        this.modelid = modelid;
    }

    public String getManufacturername() {
        return manufacturername;
    }

    public void setManufacturername(String manufacturername) {
        this.manufacturername = manufacturername;
    }

    public String getProductname() {
        return productname;
    }

    public void setProductname(String productname) {
        this.productname = productname;
    }

    public Capabilities getCapabilities() {
        return capabilities;
    }

    public void setCapabilities(Capabilities capabilities) {
        this.capabilities = capabilities;
    }

    public Config getConfig() {
        return config;
    }

    public void setConfig(Config config) {
        this.config = config;
    }

    public String getUniqueid() {
        return uniqueid;
    }

    public void setUniqueid(String uniqueid) {
        this.uniqueid = uniqueid;
    }

    public String getSwversion() {
        return swversion;
    }

    public void setSwversion(String swversion) {
        this.swversion = swversion;
    }

    public String getSwconfigid() {
        return swconfigid;
    }

    public void setSwconfigid(String swconfigid) {
        this.swconfigid = swconfigid;
    }

    public String getProductid() {
        return productid;
    }

    public void setProductid(String productid) {
        this.productid = productid;
    }

    public Map<String, Object> getAdditionalProperties() {
        return this.additionalProperties;
    }

    public void setAdditionalProperty(String name, Object value) {
        this.additionalProperties.put(name, value);
    }
}