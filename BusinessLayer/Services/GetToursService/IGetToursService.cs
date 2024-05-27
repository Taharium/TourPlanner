using Models;

namespace BusinessLayer.Services.GetToursService;

public interface IGetToursService
{
    Task<IEnumerable<Tour>> GetTours();
}