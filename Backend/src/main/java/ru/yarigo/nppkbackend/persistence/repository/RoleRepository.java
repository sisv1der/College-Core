package ru.yarigo.nppkbackend.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.nppkbackend.persistence.model.Role;

public interface RoleRepository extends JpaRepository<Role, Long> {
}
