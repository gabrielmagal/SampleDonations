package br.com.sample_donations.model.repository;

import br.com.sample_donations.model.entity.ReceiveEntity;
import br.com.sample_donations.model.interfaces.IReceiveRepository;
import io.quarkus.hibernate.orm.panache.PanacheRepositoryBase;
import jakarta.enterprise.context.RequestScoped;
import jakarta.transaction.Transactional;

import java.util.List;

@RequestScoped
@Transactional
public class ReceiveRepository implements IReceiveRepository, PanacheRepositoryBase<ReceiveEntity, Long> {
    public ReceiveEntity create(ReceiveEntity receiveEntity) {
        persistAndFlush(receiveEntity);
        return receiveEntity;
    }

    public List<ReceiveEntity> getAll() {
        return listAll();
    }

    public ReceiveEntity getById(Long id) {
        return findById(id);
    }

    public void update(ReceiveEntity receiveEntity) {
        var receive = getById(receiveEntity.getId());
        receive.setName(receiveEntity.getName());
        receive.setQuantity(receiveEntity.getQuantity());
        receive.setTypeOfDonationEnum(receiveEntity.getTypeOfDonationEnum());
        receive.setValidity(receiveEntity.getValidity());
        receive.setDateTimeReceipt(receiveEntity.getDateTimeReceipt());
        persistAndFlush(receive);
    }

    public void remove(Long id) {
        var receiveEntity = getById(id);
        delete(receiveEntity);
    }
}