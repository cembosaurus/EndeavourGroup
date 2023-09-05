using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Services.Trolley.Models;



namespace Trolley.TrolleyBusinessLogic.Interfaces
{
    public interface ITrolleyBusinessLogic
    {
        Task<IServiceResult<int>> AddAmountToStock(int productId, int amount);
        Task<IServiceResult<IEnumerable<TrolleyProduct>>> AddProductsToTrolley(Trolley_model trolley, IEnumerable<TrolleyProduct> source);
        Task GetCatalogueProductsIntoTrolley(ICollection<TrolleyProductReadDTO> products);
        Task<IServiceResult<int>> RemoveAmountFromStock(int productId, int amount);
        Task<IServiceResult<IEnumerable<TrolleyProduct>>> RemoveProductsFromTrolley(Trolley_model trolley, IEnumerable<TrolleyProduct> source);
        Task<IServiceResult<double>> UpdateTrolleyTotal(Trolley_model trolley);

    }
}
