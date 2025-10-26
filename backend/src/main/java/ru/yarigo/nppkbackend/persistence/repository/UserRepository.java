package ru.yarigo.nppkbackend.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.nppkbackend.persistence.model.User;

public interface UserRepository extends JpaRepository<User, Long> {
    boolean existsByUsername(String username);
}
