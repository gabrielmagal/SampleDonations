package br.com.sample_donations.service.interfaces;

import br.com.sample_donations.controller.dto.SendDto;

import java.util.List;

public interface ISendService {
    SendDto create(SendDto sendDto);
    List<SendDto> findAll();
    SendDto findById(Long id);
    void update(SendDto sendDto);
    void remove(Long id);
}
