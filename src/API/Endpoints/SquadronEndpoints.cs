using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Core.Models;

using Microsoft.AspNetCore.Http.HttpResults;

namespace _132ndWebsite.API.Endpoints;

public static class SquadronEndpoints
{
    public static void MapSquadronEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/squadrons");

        // GET all squadrons
        group.MapGet("/", async (ISquadronService squadronService) =>
        {
            var squadrons = await squadronService.GetAllSquadronsAsync();
            return Results.Ok(squadrons);
        })
        .WithName("GetAllSquadrons")
        .Produces<IEnumerable<Squadron>>(StatusCodes.Status200OK);

        // GET squadron by ID
        group.MapGet("/{id:int}", async (ISquadronService squadronService, int id) =>
        {
            var squadron = await squadronService.GetSquadronByIdAsync(id);
            return squadron is not null ? Results.Ok(squadron) : Results.NotFound();
        })
        .WithName("GetSquadronById")
        .Produces<Squadron>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // POST create new squadron
        group.MapPost("/", async (ISquadronService squadronService, CreateSquadronDto squadronDto) =>
        {
            var createdSquadron = await squadronService.CreateSquadronAsync(squadronDto);
            return Results.CreatedAtRoute("GetSquadronById", new { id = createdSquadron.Id }, createdSquadron);
        })
        .WithName("CreateSquadron")
        .Produces<Squadron>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        // PUT update existing squadron
        group.MapPut("/{id:int}", async (ISquadronService squadronService, int id, UpdateSquadronDto squadronDto) =>
        {
            var updatedSquadron = await squadronService.UpdateSquadronAsync(id, squadronDto);
            return updatedSquadron is not null ? Results.Ok(updatedSquadron) : Results.NotFound();
        })
        .WithName("UpdateSquadron")
        .Produces<Squadron>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}

