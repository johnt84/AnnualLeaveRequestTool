using AnnualLeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnualLeaveRequestToolBlazorWASM.Contracts
{
    public interface IAnnualLeaveRequestClient
    {
        Task<List<int>> GetYears();
        Task<List<AnnualLeaveRequestOverviewModel>> GetRequestsForYear(int year);
        Task<AnnualLeaveRequestOverviewModel> GetRequest(int annualLeaveRequestID);
        Task<AnnualLeaveRequestOverviewModel> CreateAnnualLeaveRequest(AnnualLeaveRequestOverviewModel annualLeaveRequest);
        Task<AnnualLeaveRequestOverviewModel> UpdateAnnualLeaveRequest(AnnualLeaveRequestOverviewModel annualLeaveRequest);
        Task DeleteAnnualLeaveRequest(int annualLeaveRequestID);
    }
}
