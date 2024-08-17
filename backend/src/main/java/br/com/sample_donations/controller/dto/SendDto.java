package br.com.sample_donations.controller.dto;

import br.com.sample_donations.model.entity.ReceiveEntity;
import br.com.sample_donations.model.entity.SendEntity;
import br.com.sample_donations.model.entity.UserEntity;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class SendDto {
    private Long id;
    private ReceiveDto receive;
    private UserDto user;
    private int quantity;
    private LocalDateTime dateTimeDonation;

    public SendEntity toEntity()
    {
        var send = new SendEntity();
        send.setId(id);
        send.setReceive(receive.toEntity());
        send.setUser(user.toEntity());
        send.setQuantity(quantity);
        send.setDateTimeDonation(dateTimeDonation);
        return send;
    }
}