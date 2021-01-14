package be.pxl.smarthome.service;

import be.pxl.smarthome.builders.LightBuilder;
import be.pxl.smarthome.dao.LightDao;
import be.pxl.smarthome.dao.LightGroupDao;
import be.pxl.smarthome.models.Light;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.when;

public class LightServiceTest {

    @InjectMocks
    LightServiceImpl lightService;

    @Mock
    LightDao lightDao;
    @Mock
    LightGroupDao groupDao;
    @Mock
    LightApiService apiService;

    @BeforeEach
    public void Setup() {
        MockitoAnnotations.initMocks(this);
    }

    @Test
    public void getAllLightsShouldReturnAllLightsFromDao() {
        List<Light> lights = new ArrayList<>();
        lights.add(new LightBuilder().Build());
        lights.add(new LightBuilder().Build());

        when(lightDao.findAll()).thenReturn(lights);
        assertEquals(lightService.getAllLights().size(), 2);
    }

    @Test
    public void 
}