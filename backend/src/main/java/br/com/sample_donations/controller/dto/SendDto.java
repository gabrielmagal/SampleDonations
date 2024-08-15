package br.com.sample_donations.controller.dto;

import br.com.sample_donations.model.entity.SendEntity;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class SendDto {
    private Long id;
    private Long idProduct;
    private Long idUser;
    private int quantity;
    private LocalDateTime dateTimeDonation;

    public SendEntity toEntity()
    {
        var send = new SendEntity();
        send.setId(id);
        send.setIdProduct(idProduct);
        send.setIdUser(idUser);
        send.setQuantity(quantity);
        send.setDateTimeDonation(dateTimeDonation);
        return send;
    }
}