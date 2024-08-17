package br.com.sample_donations.model.repository;

import br.com.sample_donations.model.entity.SendEntity;
import br.com.sample_donations.model.interfaces.ISendRepository;
import io.quarkus.hibernate.orm.panache.PanacheRepositoryBase;
import jakarta.enterprise.context.RequestScoped;
import jakarta.transaction.Transactional;

import java.util.List;

@RequestScoped
@Transactional
public class SendRepository implements ISendRepository, PanacheRepositoryBase<SendEntity, Long> {
    public SendEntity create(SendEntity sendEntity) {
        persistAndFlush(sendEntity);
        return sendEntity;
    }

    public List<SendEntity> getAll() {
        return listAll();
    }

    public SendEntity getById(Long id) {
        return findById(id);
    }

    public void update(SendEntity sendEntity) {
        var send = getById(sendEntity.getId());
        send.setReceive(sendEntity.getReceive());
        send.setUser(sendEntity.getUser());
        send.setQuantity(sendEntity.getQuantity());
        send.setDateTimeDonation(sendEntity.getDateTimeDonation());
        persistAndFlush(send);
    }

    public void remove(Long id) {
        var sendEntity = getById(id);
        delete(sendEntity);
    }
}