using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnnualLeaveRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnualLeaveRequestController : Controller
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public AnnualLeaveRequestController(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        [HttpGet("{year:int}")]
        public List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year)
        {
            return _annualLeaveRequestLogic.GetRequestsForYear(year);
        }

        [HttpGet("{year:int}/{annualLeaveRequestID:int}")]
        public AnnualLeaveRequestOverviewModel Get(int year, int annualLeaveRequestID)
        {
            return _annualLeaveRequestLogic.GetRequest(year, annualLeaveRequestID);
        }

        [HttpPost]
        public int Create(AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel)
        {
            var annualLeaveRequestCreated = _annualLeaveRequestLogic.Create(createAnnualLeaveRequestCRUDModel);

            return annualLeaveRequestCreated.AnnualLeaveRequestID;
        }

        [HttpPut]
        public void Update(AnnualLeaveRequestCRUDModel annualLeaveRequestOverviewModel)
        {
            _annualLeaveRequestLogic.Update(annualLeaveRequestOverviewModel);
        }

        [HttpDelete("{annualLeaveRequestID}")]
        public void Delete(int annualLeaveRequestID)
        {
            _annualLeaveRequestLogic.Delete(annualLeaveRequestID);  
        }
    }
}
