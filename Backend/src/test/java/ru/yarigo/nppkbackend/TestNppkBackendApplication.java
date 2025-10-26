package ru.yarigo.nppkbackend;

import org.springframework.boot.SpringApplication;

public class TestNppkBackendApplication {

    public static void main(String[] args) {
        SpringApplication.from(NppkBackendApplication::main).with(TestcontainersConfiguration.class).run(args);
    }

}
