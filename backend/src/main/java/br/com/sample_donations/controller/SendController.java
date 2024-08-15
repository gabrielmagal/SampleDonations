package br.com.sample_donations.controller;

import br.com.sample_donations.controller.dto.SendDto;
import br.com.sample_donations.service.interfaces.ISendService;
import jakarta.inject.Inject;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

import java.util.List;

@Path("/entrega")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class SendController {
    @Inject
    ISendService iSendService;

    @POST
    public SendDto post(@Valid SendDto sendDto) {
        return iSendService.create(sendDto);
    }

    @GET

    public List<SendDto> get() {
        return iSendService.findAll();
    }

    @GET
    @Path("/{id}")
    public SendDto getById(@NotNull @PathParam("id") Long id) {
        return iSendService.findById(id);
    }

    @PUT
    @Path("/{id}")
    public void put(@Valid SendDto sendDto) {
        iSendService.update(sendDto);
    }

    @DELETE
    @Path("/{id}")
    public void delete(@NotNull @PathParam("id") Long id) {
        iSendService.remove(id);
    }
}
