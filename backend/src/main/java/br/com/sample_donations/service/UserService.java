package br.com.sample_donations.service;

import br.com.sample_donations.controller.dto.UserDto;
import br.com.sample_donations.model.entity.UserEntity;
import br.com.sample_donations.model.interfaces.IUserRepository;
import br.com.sample_donations.service.interfaces.IUserService;
import jakarta.enterprise.context.RequestScoped;
import jakarta.inject.Inject;

import java.util.List;
import java.util.Objects;

@RequestScoped
public class UserService implements IUserService {
    @Inject
    IUserRepository iUserRepository;

    public UserDto create(UserDto userDto)
    {
        userDto.setId(null);
        var userEntity = userDto.toEntity();
        var user = iUserRepository.create(userEntity);
        if (Objects.nonNull(user)) {
            userDto.setId(user.getId());
        }
        return userDto;
    }

    public List<UserDto> findAll()
    {
        var userEntity = iUserRepository.getAll();
        if (Objects.nonNull(userEntity))
        {
            return userEntity.stream().map(UserEntity::toDto).toList();
        }
        return null;
    }

    public UserDto findById(Long id) {
        var userEntity = iUserRepository.getById(id);
        if (Objects.nonNull(userEntity)) {
            return userEntity.toDto();
        }
        return null;
    }

    public void update(UserDto userDto) {
        iUserRepository.update(userDto.toEntity());
    }

    public void remove(Long id) {
        try
        {
            iUserRepository.remove(id);
        }
        catch (Exception ex)
        {
            var user = iUserRepository.getById(id);
            int sizeReceives = user.getReceives().size();
            int sizeSends = user.getSends().size();
            if (sizeReceives > 0 || sizeSends > 0)
            {
                throw new RuntimeException("Não foi possível remover o usuário pois existe(m) " + (sizeReceives + sizeSends)  +" pendencia(s) relacionada(s) a ele.");
            }
            throw new RuntimeException("Não foi possível efetuar a remoção. " + ex.getMessage());
        }
    }
}
