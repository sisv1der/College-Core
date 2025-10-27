package ru.yarigo.cerberus.persistence.model;

import jakarta.persistence.*;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.ToString;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "permissions")
@Data
@EqualsAndHashCode(onlyExplicitlyIncluded = true)
@ToString(onlyExplicitlyIncluded = true)
public class Permission {
    @Id
    @Column(name = "id", nullable = false)
    @EqualsAndHashCode.Include
    @ToString.Include
    private Long id;

    @Column(name = "code", nullable = false, unique = true)
    @ToString.Include
    private String code;

    @Column(name = "description")
    @ToString.Include
    private String description;

    @ManyToMany(fetch = FetchType.EAGER, mappedBy = "permissions")
    private Set<Role> roles = new HashSet<>();
}
