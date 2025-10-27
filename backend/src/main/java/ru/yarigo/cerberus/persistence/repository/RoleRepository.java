package ru.yarigo.cerberus.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.cerberus.persistence.model.Role;

public interface RoleRepository extends JpaRepository<Role, Long> {
}
