package br.com.sample_donations.model.interfaces;

import br.com.sample_donations.model.entity.SendEntity;

import java.util.List;

public interface ISendRepository {
    SendEntity create(SendEntity sendEntity);
    List<SendEntity> getAll();
    SendEntity getById(Long id);
    void update(SendEntity sendEntity);
    void remove(Long id);
}
