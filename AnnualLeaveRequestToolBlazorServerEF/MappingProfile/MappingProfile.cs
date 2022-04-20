using AutoMapper;

namespace AnnualLeaveRequestToolBlazorServerEF.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AnnualLeaveRequest.Shared.AnnualLeaveRequestOverviewModel, AnnualLeaveRequestEFDAL.Models.AnnualLeaveRequestsOverview>()
                .ReverseMap();
        }
    }
}