package be.pxl.smarthome.aspect;

import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.springframework.stereotype.Component;

@Aspect
@Component
public class LoggerAspect {
    @Before("execution(* be.pxl.smarthome.service.LightServiceImpl.getAllLights())")
    public void beforeGetAllLights(){
        System.out.println("All lights requesting");
    }
}
