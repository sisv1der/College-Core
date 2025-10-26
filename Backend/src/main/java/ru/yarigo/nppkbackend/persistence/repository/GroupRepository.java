package ru.yarigo.nppkbackend.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.nppkbackend.persistence.model.Group;

public interface GroupRepository extends JpaRepository<Group, Long> {
}
