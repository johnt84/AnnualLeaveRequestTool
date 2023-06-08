using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Queries
{
    public record GetYearsQuery() : IRequest<List<int>>;
}
