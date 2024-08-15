package br.com.sample_donations.controller;

import br.com.sample_donations.controller.dto.ReceiveDto;
import br.com.sample_donations.service.interfaces.IReceiveService;
import jakarta.inject.Inject;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

import java.util.List;

@Path("/recebimento")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ReceiveController {
    @Inject
    IReceiveService iReceiveService;

    @POST
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public ReceiveDto post(ReceiveDto receiveDto) {
        return iReceiveService.create(receiveDto);
    }

    @GET
    public List<ReceiveDto> get() {
        return iReceiveService.findAll();
    }

    @GET
    @Path("/{id}")
    public ReceiveDto getById(@NotNull @PathParam("id") Long id) {
        return iReceiveService.findById(id);
    }

    @PUT
    @Path("/{id}")
    public void put(@Valid ReceiveDto receiveDto) {
        iReceiveService.update(receiveDto);
    }

    @DELETE
    @Path("/{id}")
    public void delete(@NotNull @PathParam("id") Long id) {
        iReceiveService.remove(id);
    }
}
