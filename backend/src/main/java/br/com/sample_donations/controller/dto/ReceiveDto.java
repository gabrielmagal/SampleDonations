package br.com.sample_donations.controller.dto;

import br.com.sample_donations.model.entity.ReceiveEntity;
import br.com.sample_donations.service.enums.TypeOfDonationEnum;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class ReceiveDto {
    private Long id;

    private String name;

    private TypeOfDonationEnum typeOfDonationEnum;

    private int quantity;

    private LocalDateTime validity;
    private LocalDateTime dateTimeReceipt;
    private UserDto user;

    public ReceiveEntity toEntity()
    {
        var receive = new ReceiveEntity();
        receive.setId(id);
        receive.setName(name);
        receive.setTypeOfDonationEnum(typeOfDonationEnum);
        receive.setQuantity(quantity);
        receive.setValidity(validity);
        receive.setDateTimeReceipt(dateTimeReceipt);
        receive.setUser(user.toEntity());
        return receive;
    }
}