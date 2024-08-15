package br.com.sample_donations.model.interfaces;

import br.com.sample_donations.model.entity.ReceiveEntity;

import java.util.List;

public interface IReceiveRepository {
    ReceiveEntity create(ReceiveEntity receive);
    List<ReceiveEntity> getAll();
    ReceiveEntity getById(Long id);
    void update(ReceiveEntity receive);
    void remove(Long id);
}
