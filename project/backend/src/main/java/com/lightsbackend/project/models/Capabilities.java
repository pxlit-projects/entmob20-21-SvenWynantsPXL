package com.lightsbackend.project.models;

public class Capabilities {
    private boolean certified;
    private Control control;
    private Streaming streaming;

    public Capabilities(boolean certified, Control control, Streaming streaming) {
        this.certified = certified;
        this.control = control;
        this.streaming = streaming;
    }

    public Capabilities() {
    }

    public boolean isCertified() {
        return certified;
    }

    public void setCertified(boolean certified) {
        this.certified = certified;
    }

    public Control getControl() {
        return control;
    }

    public void setControl(Control control) {
        this.control = control;
    }

    public Streaming getStreaming() {
        return streaming;
    }

    public void setStreaming(Streaming streaming) {
        this.streaming = streaming;
    }
}
