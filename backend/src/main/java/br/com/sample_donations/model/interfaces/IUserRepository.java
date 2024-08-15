package br.com.sample_donations.model.interfaces;

import br.com.sample_donations.model.entity.UserEntity;

import java.util.List;

public interface IUserRepository {
    UserEntity create(UserEntity userEntity);
    List<UserEntity> getAll();
    UserEntity getById(Long id);
    void update(UserEntity userEntity);
    void remove(Long id);
}
