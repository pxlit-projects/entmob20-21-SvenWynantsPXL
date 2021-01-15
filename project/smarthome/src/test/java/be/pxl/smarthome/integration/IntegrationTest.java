package be.pxl.smarthome.integration;

import be.pxl.smarthome.dto.CreateGroupDto;
import be.pxl.smarthome.models.LightGroup;
import be.pxl.smarthome.service.GroupService;
import be.pxl.smarthome.service.LightService;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;

import javax.transaction.Transactional;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class IntegrationTest {

    @Autowired
    GroupService groupService;

    @Autowired
    LightService lightService;

    @Autowired
    MockMvc mockMvc;

    @Test
    @Transactional
    @WithMockUser(username = "sven", password = "pxl", authorities = "ROLE_ADMIN")
    public void createGroupAndGetGroupTest() throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        CreateGroupDto createGroupDto = new CreateGroupDto();
        createGroupDto.Name = "Group test";

        MvcResult result = mockMvc.perform(post("/groups/addGroup")
                                    .contentType("application/json")
                                    .content(mapper.writeValueAsString(createGroupDto)))
                                    .andExpect(status().isOk())
                                    .andReturn();

        String content = result.getResponse().getContentAsString();

        LightGroup addedGroup = mapper.readValue(content, LightGroup.class);
        LightGroup requestedGroup = groupService.findLightGroupById(addedGroup.getId()).orElse(null);

        assertNotNull(requestedGroup);
        assertEquals(addedGroup.getName(), requestedGroup.getName());
    }
}
