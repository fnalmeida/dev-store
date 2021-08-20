using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DevStore.Bff.Checkout.Extensions;
using DevStore.Bff.Checkout.Models;
using Microsoft.Extensions.Options;

namespace DevStore.Bff.Checkout.Services
{
    public interface IClientService
    {
        Task<AddressDto> GetAddress();
    }

    public class ClientService : Service, IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
        }

        public async Task<AddressDto> GetAddress()
        {
            var response = await _httpClient.GetAsync("/clients/address");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            ManageHttpResponse(response);

            return await DeserializeResponse<AddressDto>(response);
        }
    }
}