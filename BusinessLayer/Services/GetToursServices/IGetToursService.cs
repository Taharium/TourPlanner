using Models;

namespace BusinessLayer.Services.GetToursServices;

public interface IGetToursService
{
    Task<IEnumerable<Tour>> GetTours();
}