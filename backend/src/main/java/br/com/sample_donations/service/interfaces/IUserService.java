package br.com.sample_donations.service.interfaces;

import br.com.sample_donations.controller.dto.UserDto;

import java.util.List;

public interface IUserService {
    UserDto create(UserDto userDto);
    List<UserDto> findAll();
    UserDto findById(Long id);
    void update(UserDto userDto);
    void remove(Long id);
}
