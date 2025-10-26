package ru.yarigo.nppkbackend.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.nppkbackend.persistence.model.Profile;

public interface ProfileRepository extends JpaRepository<Profile, Long> {
}
