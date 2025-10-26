package ru.yarigo.nppkbackend.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.nppkbackend.persistence.model.Absence;

public interface AbsenceRepository extends JpaRepository<Absence, Long> {
}
