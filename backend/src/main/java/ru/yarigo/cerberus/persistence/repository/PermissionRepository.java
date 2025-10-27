package ru.yarigo.cerberus.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.cerberus.persistence.model.Permission;

public interface PermissionRepository extends JpaRepository<Permission, Long> {
}
