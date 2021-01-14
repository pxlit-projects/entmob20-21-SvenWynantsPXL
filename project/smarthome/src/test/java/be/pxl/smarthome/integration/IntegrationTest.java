package be.pxl.smarthome.integration;

import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.web.servlet.MockMvc;

public class IntegrationTest {
    @Autowired
    GroupService groupService;
    @Autowired
    LightService lightService;

    @Autowired
    MockMvc mockMvc;

    @Test
    public void createGroupAndGetAllGroupsTest(){
        
    }
}
