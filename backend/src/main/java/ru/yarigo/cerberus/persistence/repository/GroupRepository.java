package ru.yarigo.cerberus.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.cerberus.persistence.model.Group;

public interface GroupRepository extends JpaRepository<Group, Long> {
}
