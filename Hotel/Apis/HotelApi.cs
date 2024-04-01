using HotelMinimalWebApi.Auth;
using Microsoft.AspNetCore.Authorization;

namespace HotelMinimalWebApi.Apis
{
    public class HotelApi: IApi
    {
        public void Register(WebApplication app)
        {
                    var builder = WebApplication.CreateBuilder();
                    app.MapGet("/hotels", [Authorize] async (IHotelRepository repository) => Results.Ok(await repository.GetHotelsAsync()));
                    app.MapGet("/hotels/{id}", [Authorize] async (int id, IHotelRepository repository) => await repository.GetHotelsAsync(id) is Hotel hotel
                    ? Results.Ok(hotel)
                    : Results.NotFound());
                    app.MapPost("/hotels", [Authorize] async ([FromBody] Hotel hotel, [FromServices] IHotelRepository repository) =>
                    {
                        await repository.InsertHotelAsync(hotel);
                        await repository.SaveAsync();
                        Results.CreatedAtRoute($"/hotels/{hotel.Id}", hotel);
                    });

                    app.MapPut("/hotels", [Authorize] async ([FromBody] Hotel hotel, [FromServices] IHotelRepository repository) =>
                    {
                        await repository.UpdateHotelAsync(hotel);
                        await repository.SaveAsync();
                        return Results.NoContent();
                    });
                    app.MapDelete("/hotels/{id}", [Authorize] async (int id, IHotelRepository repository) =>
                    {
                        await repository.DeleteHotelAsync(id);
                        await repository.SaveAsync();

                        return Results.NoContent();
                    });

            // Configure the HTTP request pipeline.

            
        }
    }
}
