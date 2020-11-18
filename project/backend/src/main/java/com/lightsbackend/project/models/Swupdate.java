package com.lightsbackend.project.models;

import java.time.LocalDateTime;

public class Swupdate {
    private String state;
    private LocalDateTime lastinstall;

    public Swupdate(String state, LocalDateTime lastinstall) {
        this.state = state;
        this.lastinstall = lastinstall;
    }

    public Swupdate() {
    }

    public String getState() {
        return state;
    }

    public void setState(String state) {
        this.state = state;
    }

    public LocalDateTime getLastinstall() {
        return lastinstall;
    }

    public void setLastinstall(LocalDateTime lastinstall) {
        this.lastinstall = lastinstall;
    }
}
