package ru.yarigo.cerberus.persistence.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.yarigo.cerberus.persistence.model.Profile;

public interface ProfileRepository extends JpaRepository<Profile, Long> {
}
