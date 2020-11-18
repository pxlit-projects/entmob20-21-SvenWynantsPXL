package com.lightsbackend.project.models;

public class Streaming {
    private boolean renderer;
    private boolean proxy;

    public Streaming(boolean renderer, boolean proxy) {
        this.renderer = renderer;
        this.proxy = proxy;
    }

    public Streaming() {
    }

    public boolean isRenderer() {
        return renderer;
    }

    public void setRenderer(boolean renderer) {
        this.renderer = renderer;
    }

    public boolean isProxy() {
        return proxy;
    }

    public void setProxy(boolean proxy) {
        this.proxy = proxy;
    }
}
