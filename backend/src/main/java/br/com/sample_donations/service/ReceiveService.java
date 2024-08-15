package br.com.sample_donations.service;

import br.com.sample_donations.controller.dto.ReceiveDto;
import br.com.sample_donations.model.entity.ReceiveEntity;
import br.com.sample_donations.model.interfaces.IReceiveRepository;
import br.com.sample_donations.service.interfaces.IReceiveService;
import jakarta.enterprise.context.RequestScoped;
import jakarta.inject.Inject;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Objects;

@RequestScoped
public class ReceiveService implements IReceiveService {
    @Inject
    IReceiveRepository iReceiveRepository;

    public ReceiveDto create(ReceiveDto receiveDto)
    {
        receiveDto.setId(null);
        var receiveEntity = receiveDto.toEntity();
        receiveEntity.setDateTimeReceipt(LocalDateTime.now());
        var receive = iReceiveRepository.create(receiveEntity);
        if (Objects.nonNull(receive)) {
            receiveDto.setId(receive.getId());
        }
        return receiveDto;
    }

    public List<ReceiveDto> findAll()
    {
        var receiveEntity = iReceiveRepository.getAll();
        if (Objects.nonNull(receiveEntity))
        {
            return receiveEntity.stream().map(ReceiveEntity::toDto).toList();
        }
        return null;
    }

    public ReceiveDto findById(Long id) {
        var receiveEntity = iReceiveRepository.getById(id);
        if (Objects.nonNull(receiveEntity)) {
            return receiveEntity.toDto();
        }
        return null;
    }

    public void update(ReceiveDto receiveDto) {
        iReceiveRepository.update(receiveDto.toEntity());
    }

    public void remove(Long id) {
        iReceiveRepository.remove(id);
    }
}
