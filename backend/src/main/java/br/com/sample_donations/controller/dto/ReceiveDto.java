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

    @NotBlank(message = "Nome não pode ser vazio.")
    @Size(min = 5, max = 40, message = "O nome deve ter um tamanho entre 5 e 40 digitos.")
    private String name;

    @NotNull(message = "O tipo de alimento é obrigatório.")
    private TypeOfDonationEnum typeOfDonationEnum;

    @Min(value = 1, message = "É necessário ao menos 1 item.")
    private int quantity;

    @NotNull(message = "Validade não pode ser vazia.")
    private LocalDate validity;

    private LocalDateTime dateTimeReceipt;

    public ReceiveEntity toEntity()
    {
        var receive = new ReceiveEntity();
        receive.setId(id);
        receive.setName(name);
        receive.setTypeOfDonationEnum(typeOfDonationEnum);
        receive.setQuantity(quantity);
        receive.setValidity(validity);
        receive.setDateTimeReceipt(dateTimeReceipt);
        return receive;
    }
}