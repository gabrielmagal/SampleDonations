package br.com.sample_donations.model.entity;

import br.com.sample_donations.controller.dto.SendDto;
import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@EqualsAndHashCode(callSuper = true)
public class SendEntity extends PanacheEntityBase {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "receive_id", nullable = false)
    private ReceiveEntity receive;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "user_id", nullable = false)
    private UserEntity user;

    private int quantity;
    private LocalDateTime dateTimeDonation;

    @Transient
    public SendDto toDto()
    {
        var sendDto = new SendDto();
        sendDto.setId(id);
        sendDto.setReceive(receive.toDto());
        sendDto.setUser(user.toDto());
        sendDto.setQuantity(quantity);
        sendDto.setDateTimeDonation(dateTimeDonation);
        return sendDto;
    }
}