package br.com.sample_donations.model.repository;

import br.com.sample_donations.model.entity.UserEntity;
import br.com.sample_donations.model.interfaces.IUserRepository;
import io.quarkus.hibernate.orm.panache.PanacheRepositoryBase;
import jakarta.enterprise.context.RequestScoped;
import jakarta.transaction.Transactional;

import java.util.List;

@RequestScoped
@Transactional
public class UserRepository implements IUserRepository, PanacheRepositoryBase<UserEntity, Long> {
    public UserEntity create(UserEntity userEntity) {
        persistAndFlush(userEntity);
        return userEntity;
    }

    public List<UserEntity> getAll() {
        return listAll();
    }

    public UserEntity getById(Long id) {
        return findById(id);
    }

    public void update(UserEntity userEntity) {
        var user = getById(userEntity.getId());
        user.setName(userEntity.getName());
        user.setEmail(userEntity.getEmail());
        user.setCpf(userEntity.getCpf());
        user.setPhoto(userEntity.getPhoto());
        user.setDateOfBirth(userEntity.getDateOfBirth());
        user.setUserPermissionTypeEnum(userEntity.getUserPermissionTypeEnum());
        persistAndFlush(user);
    }

    public void remove(Long id) {
        var receiveEntity = getById(id);
        delete(receiveEntity);
    }
}