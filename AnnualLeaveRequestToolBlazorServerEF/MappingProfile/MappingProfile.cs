using AutoMapper;
using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestEFDAL.Models;

namespace AnnualLeaveRequestToolBlazorServerEF.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AnnualLeaveRequestOverviewModel, AnnualLeaveRequestsOverview>()
                .ReverseMap();
        }
    }
}