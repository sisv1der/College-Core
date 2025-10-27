package ru.yarigo.cerberus.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.cerberus.persistence.model.Absence;

public interface AbsenceRepository extends JpaRepository<Absence, Long> {
}
