package br.com.sample_donations.service;

import br.com.sample_donations.controller.dto.SendDto;
import br.com.sample_donations.model.entity.SendEntity;
import br.com.sample_donations.model.interfaces.IReceiveRepository;
import br.com.sample_donations.model.interfaces.ISendRepository;
import br.com.sample_donations.service.interfaces.ISendService;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;

import java.util.List;
import java.util.Objects;

@ApplicationScoped
public class SendService implements ISendService {
    @Inject
    ISendRepository iSendRepository;

    @Inject
    IReceiveRepository iReceiveRepository;

    public SendDto create(SendDto sendDto)
    {
        var receive = iReceiveRepository.getById(sendDto.getIdProduct());

        sendDto.setId(null);
        var sendEntity = sendDto.toEntity();
        var send = iSendRepository.create(sendEntity);
        if (Objects.nonNull(send)) {
            sendDto.setId(sendEntity.getId());
        }

        receive.setQuantity(receive.getQuantity() - sendDto.getQuantity());
        iReceiveRepository.update(receive);

        return sendDto;
    }

    public List<SendDto> findAll()
    {
        var sendEntity = iSendRepository.getAll();
        if (Objects.nonNull(sendEntity))
        {
            return sendEntity.stream().map(SendEntity::toDto).toList();
        }
        return null;
    }

    public SendDto findById(Long id) {
        var sendEntity = iSendRepository.getById(id);
        if (Objects.nonNull(sendEntity)) {
            return sendEntity.toDto();
        }
        return null;
    }

    public void update(SendDto sendDto) {
        var sendEntity = iSendRepository.getById(sendDto.getId());

        var receiveEntity = iReceiveRepository.getById(sendDto.getIdProduct());
        receiveEntity.setQuantity(receiveEntity.getQuantity() + sendEntity.getQuantity());
        iReceiveRepository.update(receiveEntity);

        receiveEntity.setQuantity(receiveEntity.getQuantity() - sendDto.getQuantity());

        iSendRepository.update(sendDto.toEntity());

        iReceiveRepository.update(receiveEntity);
    }

    public void remove(Long id) {
        var sendEntity = iSendRepository.getById(id);

        var receiveEntity = iReceiveRepository.getById(sendEntity.getIdProduct());
        receiveEntity.setQuantity(receiveEntity.getQuantity() + sendEntity.getQuantity());
        iReceiveRepository.update(receiveEntity);

        iSendRepository.remove(id);
    }
}
