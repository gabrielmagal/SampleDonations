package br.com.sample_donations.controller;

import br.com.sample_donations.controller.dto.UserDto;
import br.com.sample_donations.service.interfaces.IUserService;
import jakarta.inject.Inject;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

import java.util.List;

@Path("/usuario")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class UserController {
    @Inject
    IUserService iUserService;

    @POST
    public UserDto post(@Valid UserDto userDto) {
        return iUserService.create(userDto);
    }

    @GET

    public List<UserDto> get() {
        return iUserService.findAll();
    }

    @GET
    @Path("/{id}")
    public UserDto getById(@NotNull @PathParam("id") Long id) {
        return iUserService.findById(id);
    }

    @PUT
    @Path("/{id}")
    public void put(@Valid UserDto userDto) {
        iUserService.update(userDto);
    }

    @DELETE
    @Path("/{id}")
    public void delete(@NotNull @PathParam("id") Long id) {
        iUserService.remove(id);
    }
}
