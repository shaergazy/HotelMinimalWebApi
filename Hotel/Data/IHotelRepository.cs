public interface IHotelRepository:IDisposable
{
   Task<List<Hotel>> GetHotelsAsync();
    Task<Hotel> GetHotelsAsync(int hotelId);
    Task InsertHotelAsync(Hotel hotel);
    Task UpdateHotelAsync(Hotel hotel);
    Task DeleteHotelAsync(int hotelId);
    Task SaveAsync();

}