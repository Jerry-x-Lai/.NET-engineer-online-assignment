using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoInfoApi.Services
{
    public interface ICoindeskService
    {
        Task<string> GetCurrentPriceJsonAsync();
    }
}
