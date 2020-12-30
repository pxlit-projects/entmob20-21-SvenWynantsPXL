package be.pxl.smarthome.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(HttpStatus.BAD_REQUEST)
public class EntityAlreadyExistsException extends RuntimeException {
    public EntityAlreadyExistsException(String name) {
        super("Entity with name: " + name + " already exists.");
    }
}
