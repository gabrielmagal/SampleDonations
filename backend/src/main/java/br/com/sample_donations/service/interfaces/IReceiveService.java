package br.com.sample_donations.service.interfaces;

import br.com.sample_donations.controller.dto.ReceiveDto;

import java.util.List;

public interface IReceiveService {
    ReceiveDto create(ReceiveDto receiveDto);
    List<ReceiveDto> findAll();
    ReceiveDto findById(Long id);
    void update(ReceiveDto receiveDto);
    void remove(Long id);
}
