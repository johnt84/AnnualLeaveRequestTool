using MediatR;
using System.Collections.Generic;

namespace AnnualLeaveRequestAPI.Queries
{
    public record GetYearsQuery() : IRequest<List<int>>;
}
