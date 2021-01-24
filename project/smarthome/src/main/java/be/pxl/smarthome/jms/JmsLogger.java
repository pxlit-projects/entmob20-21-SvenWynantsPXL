package be.pxl.smarthome.jms;

import org.springframework.jms.annotation.JmsListener;
import org.springframework.stereotype.Component;

@Component
public class JmsLogger {
    @JmsListener(destination = "lightListener", containerFactory = "myFactory")
    public void receiveLightMessage(String message){
        System.out.println(message);
    }

    @JmsListener(destination = "sunset", containerFactory = "myFactory")
    public void receiveSunsetMessage(String message) {
        System.out.println(message);
    }
}
