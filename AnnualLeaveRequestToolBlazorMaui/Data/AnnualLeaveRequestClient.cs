using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestToolBlazorMaui.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace AnnualLeaveRequestToolBlazorMaui.Data
{
    public class AnnualLeaveRequestClient : IAnnualLeaveRequestClient
    {
        private readonly HttpClient _httpClient;
        public readonly IConfiguration _configuration;

        private static string CONTROLLER_NAME = "AnnualLeaveRequest";

        public AnnualLeaveRequestClient(HttpClient client)//, IConfiguration configruation)
        {
            _httpClient = client;
            //_configuration = configruation;

            //_httpClient.BaseAddress = new Uri(_configuration["AnnualLeaveRequestAPI"]);
        }

        public async Task<List<int>> GetYears() =>
            await _httpClient.GetFromJsonAsync<List<int>>($"{CONTROLLER_NAME}/GetYears");

        public async Task<List<AnnualLeaveRequestOverviewModel>> GetRequestsForYear(int year) =>
            await _httpClient.GetFromJsonAsync<List<AnnualLeaveRequestOverviewModel>>($"{CONTROLLER_NAME}/GetRequestsForYear/{year}");

        public async Task<AnnualLeaveRequestOverviewModel> GetRequest(int annualLeaveRequestID) =>
            await _httpClient.GetFromJsonAsync<AnnualLeaveRequestOverviewModel>($"{CONTROLLER_NAME}/Get/{annualLeaveRequestID}");

        public async Task<AnnualLeaveRequestOverviewModel> CreateAnnualLeaveRequest(AnnualLeaveRequestOverviewModel annualLeaveRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(CONTROLLER_NAME, annualLeaveRequest);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }

            return await response.Content.ReadFromJsonAsync<AnnualLeaveRequestOverviewModel>();
        }

        public async Task<AnnualLeaveRequestOverviewModel> UpdateAnnualLeaveRequest(AnnualLeaveRequestOverviewModel annualLeaveRequest)
        {
            var response = await _httpClient.PutAsJsonAsync(CONTROLLER_NAME, annualLeaveRequest);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }

            return await response.Content.ReadFromJsonAsync<AnnualLeaveRequestOverviewModel>();
        }

        public async Task DeleteAnnualLeaveRequest(int annualLeaveRequestID)
        {
            var response = await _httpClient.DeleteAsync($"{CONTROLLER_NAME}/{annualLeaveRequestID}");

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }
        }
    }
}
