package be.pxl.smarthome;

import be.pxl.smarthome.service.LightApiService;
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

@SpringBootApplication
@EnableGlobalMethodSecurity(securedEnabled = true)
public class SmarthomeApplication extends WebSecurityConfigurerAdapter {

    @Autowired
    private static LightApiService apiService;

    public static void main(String[] args) {
        SpringApplication.run(SmarthomeApplication.class, args);
        apiService.getAllLightsInNetwork();
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
