using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetRequestsForYear(int year)
        {
            try
            {
                var annualLeaveRequestsForYear = _annualLeaveRequestLogic.GetRequestsForYear(year);

                if (annualLeaveRequestsForYear != null && annualLeaveRequestsForYear.Count > 0)
                {
                    return Ok(annualLeaveRequestsForYear);
                }
                else
                {
                    return BadRequest($"No requests exist for year: {year}");
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving requests for year: {year}");
            }
        }

        [HttpGet("{year:int}/{annualLeaveRequestID:int}")]
        public IActionResult Get(int year, int annualLeaveRequestID)
        {
            try
            {
                var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(year, annualLeaveRequestID);

                if (annualLeaveRequest != null && annualLeaveRequest.Year == year)
                {
                    return Ok(annualLeaveRequest);
                }
                else
                {
                    return BadRequest($"No request exists for year: {year} and annualLeaveRequestID: {annualLeaveRequestID}");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving request for year: {year} and annualLeaveRequestID: {annualLeaveRequestID}");
            }

            
        }

        [HttpPost]
        public ActionResult<AnnualLeaveRequestCRUDModel> Create(AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel)
        {
            try
            {
                var annualLeaveRequestCreated = _annualLeaveRequestLogic.Create(createAnnualLeaveRequestCRUDModel);

                if (annualLeaveRequestCreated != null && annualLeaveRequestCreated.Year == createAnnualLeaveRequestCRUDModel.Year)
                {
                    return CreatedAtAction(nameof(Create),
                        new { id = annualLeaveRequestCreated.AnnualLeaveRequestID }, annualLeaveRequestCreated);
                }
                else
                {
                    if (annualLeaveRequestCreated == null || annualLeaveRequestCreated.Year != createAnnualLeaveRequestCRUDModel.Year)
                    {
                        return BadRequest($"Annual Leave Request not created");
                    }
                    else if (!string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
                    {
                        return BadRequest($"Annual Leave Request not created.  Error Messages: {annualLeaveRequestCreated.ErrorMessage}");
                    }
                    else
                    {
                        return BadRequest($"Annual leave request was not created");
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error creating the annual leave request");
            }

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
