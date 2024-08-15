package br.com.sample_donations.controller.dto;

import br.com.sample_donations.model.entity.UserEntity;
import br.com.sample_donations.service.enums.UserPermissionTypeEnum;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class UserDto {
    private Long id;
    private String name;
    private String password;
    private String email;
    private String cpf;
    private String photo;
    private LocalDate dateOfBirth;
    private UserPermissionTypeEnum userPermissionTypeEnum;

    public UserEntity toEntity()
    {
        var user = new UserEntity();
        user.setId(id);
        user.setName(name);
        user.setEmail(email);
        user.setCpf(cpf);
        user.setPhoto(photo);
        user.setDateOfBirth(dateOfBirth);
        user.setUserPermissionTypeEnum(userPermissionTypeEnum);
        return user;
    }
}
