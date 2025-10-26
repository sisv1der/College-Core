package ru.yarigo.nppkbackend.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.nppkbackend.persistence.model.Permission;

public interface PermissionRepository extends JpaRepository<Permission, Long> {
}
