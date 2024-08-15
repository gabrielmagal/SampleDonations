package br.com.sample_donations.model.entity;

import br.com.sample_donations.controller.dto.UserDto;
import br.com.sample_donations.service.enums.UserPermissionTypeEnum;
import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@EqualsAndHashCode(callSuper = true)
public class UserEntity extends PanacheEntityBase {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;
    private String email;
    private String cpf;
    private String photo;
    private LocalDate dateOfBirth;
    private UserPermissionTypeEnum userPermissionTypeEnum;

    @Transient
    public UserDto toDto()
    {
        var userDto = new UserDto();
        userDto.setId(id);
        userDto.setName(name);
        userDto.setEmail(email);
        userDto.setCpf(cpf);
        userDto.setPhoto(photo);
        userDto.setDateOfBirth(dateOfBirth);
        userDto.setUserPermissionTypeEnum(userPermissionTypeEnum);
        return userDto;
    }
}