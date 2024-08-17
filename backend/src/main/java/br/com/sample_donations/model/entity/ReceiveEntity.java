package br.com.sample_donations.model.entity;

import br.com.sample_donations.controller.dto.ReceiveDto;
import br.com.sample_donations.service.enums.TypeOfDonationEnum;
import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.time.LocalDateTime;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@EqualsAndHashCode(callSuper = true)
public class ReceiveEntity extends PanacheEntityBase {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;
    private TypeOfDonationEnum typeOfDonationEnum;
    private int quantity;
    private LocalDateTime validity;
    private LocalDateTime dateTimeReceipt = LocalDateTime.now();

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "user_id", nullable = false)
    private UserEntity user;

    @Transient
    public ReceiveDto toDto()
    {
        var receiveDto = new ReceiveDto();
        receiveDto.setId(id);
        receiveDto.setName(name);
        receiveDto.setTypeOfDonationEnum(typeOfDonationEnum);
        receiveDto.setQuantity(quantity);
        receiveDto.setValidity(validity);
        receiveDto.setDateTimeReceipt(dateTimeReceipt);
        receiveDto.setUser(user.toDto());
        return receiveDto;
    }
}