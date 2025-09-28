using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Core.Models;

using Microsoft.AspNetCore.Http.HttpResults;

namespace _132ndWebsite.API.Endpoints;

public static class SquadronEndpoints
{
    public static void MapSquadronEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/squadrons");

        group.MapGet("/", async (ISquadronService squadronService) =>
        {
            var squadrons = await squadronService.GetAllSquadronsAsync();
            return Results.Ok(squadrons);
        })
        .WithName("GetAllSquadrons")
        .Produces<IEnumerable<Squadron>>(StatusCodes.Status200OK);

        group.MapGet("/{id:int}", async (ISquadronService squadronService, int id) =>
        {
            var squadron = await squadronService.GetSquadronByIdAsync(id);
            return squadron is not null ? Results.Ok(squadron) : Results.NotFound();
        })
        .WithName("GetSquadronById")
        .Produces<Squadron>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}

