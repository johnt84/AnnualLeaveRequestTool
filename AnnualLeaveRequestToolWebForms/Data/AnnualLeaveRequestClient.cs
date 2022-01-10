using AnnualLeaveRequestToolWebForms.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnnualLeaveRequestToolWebForms.Data
{
    public class AnnualLeaveRequestClient : IAnnualLeaveRequestLogic
    {
        private readonly HttpClient _httpClient;

        private static string CONTROLLER_NAME = "AnnualLeaveRequest";

        public AnnualLeaveRequestClient()
        {
            _httpClient = GlobalSettings.HttpClient;
        }

        public async Task<List<int>> GetYearsAsync()
        {
            string jsonResult = await _httpClient.GetStringAsync($"{CONTROLLER_NAME}/GetYears");
            return JsonConvert.DeserializeObject<List<int>>(jsonResult);
        }

        public async Task<List<AnnualLeaveRequestOverviewModel>> GetRequestsForYearAsync(int year)
        {
            string jsonResult = await _httpClient.GetStringAsync($"{CONTROLLER_NAME}/GetRequestsForYear/{year}");
            return JsonConvert.DeserializeObject<List<AnnualLeaveRequestOverviewModel>>(jsonResult);
        }

        public async Task<AnnualLeaveRequestOverviewModel> GetRequestAsync(int annualLeaveRequestID)
        {
            string jsonResult = await _httpClient.GetStringAsync($"{CONTROLLER_NAME}/Get/{annualLeaveRequestID}");
            return JsonConvert.DeserializeObject<AnnualLeaveRequestOverviewModel>(jsonResult);
        }

        public async Task<AnnualLeaveRequestOverviewModel> CreateAsync(AnnualLeaveRequestOverviewModel annualLeaveRequest)
        {
            var annualLeaveRequestJSON = JsonConvert.SerializeObject(annualLeaveRequest);

            var content = new StringContent(annualLeaveRequestJSON, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(CONTROLLER_NAME, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AnnualLeaveRequestOverviewModel>(jsonResult);
        }

        public async Task<AnnualLeaveRequestOverviewModel> UpdateAsync(AnnualLeaveRequestOverviewModel annualLeaveRequest)
        {
            var annualLeaveRequestJSON = JsonConvert.SerializeObject(annualLeaveRequest);

            var content = new StringContent(annualLeaveRequestJSON, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(CONTROLLER_NAME, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AnnualLeaveRequestOverviewModel>(jsonResult);
        }

        public async Task DeleteAsync(AnnualLeaveRequestOverviewModel annualLeaveRequestD)
        {
            var response = await _httpClient.DeleteAsync($"{CONTROLLER_NAME}/{annualLeaveRequestD.AnnualLeaveRequestID}");

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }
        }
    }
}