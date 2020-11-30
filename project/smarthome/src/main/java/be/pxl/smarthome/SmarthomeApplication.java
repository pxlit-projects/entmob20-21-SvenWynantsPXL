package be.pxl.smarthome;

import be.pxl.smarthome.models.Light;
import be.pxl.smarthome.service.LightApiService;
import be.pxl.smarthome.service.LightService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

import javax.sql.DataSource;
import java.util.List;

@SpringBootApplication
@EnableGlobalMethodSecurity(securedEnabled = true)
public class SmarthomeApplication extends WebSecurityConfigurerAdapter {

    @Autowired
    private static LightApiService apiService;
    @Autowired
    private static LightService lightService;

    public static void main(String[] args) {
        SpringApplication.run(SmarthomeApplication.class, args);
        List<Light> apiLights = apiService.getAllLightsInNetwork();
        List<Light> myLights = lightService.getAllLights();

        for (Light light : apiLights) {
            if (!myLights.contains(light)) {
                lightService.addLight(light);
            }
        }
    }

    @Override
    public void configure(HttpSecurity http) throws Exception {
        http
                .formLogin().disable()
                .httpBasic()
                .and()
                .sessionManagement()
                .sessionCreationPolicy(SessionCreationPolicy.STATELESS);;
    }

    @Autowired
    public void configureSecurity(AuthenticationManagerBuilder builder,
                                  DataSource ds) throws Exception {
        builder.jdbcAuthentication()
                .passwordEncoder(new BCryptPasswordEncoder())
                .dataSource(ds)
                .usersByUsernameQuery(
                        "select name, password, enabled from Users where name = ?"
                ).authoritiesByUsernameQuery(
                "select name, role from Users where name = ?"
        );
    }
}
