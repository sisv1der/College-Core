package ru.yarigo.nppkbackend.security.model;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import ru.yarigo.nppkbackend.persistence.model.User;

import java.util.Collection;

public record UserSecurity(User user) implements UserDetails {
    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return user.getRoles().stream()
                .flatMap(
                        role -> role.getPermissions().stream()
                ).map(
                        permission -> new SimpleGrantedAuthority(permission.getCode())
                ).toList();
    }

    @Override
    public String getPassword() {
        return user.getPasswordHash();
    }

    @Override
    public String getUsername() {
        return user.getUsername();
    }
}
