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
                    return BadRequest($"No annual leave requests exist for year: {year}");
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving annual leave requests for year: {year}");
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
                    return BadRequest($"No annual leave request exists for year: {year} and annualLeaveRequestID: {annualLeaveRequestID}");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving the annual leave request for year: {year} and annualLeaveRequestID: {annualLeaveRequestID}");
            }
        }

        [HttpPost]
        public ActionResult<AnnualLeaveRequestCRUDModel> Create(AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel)
        {
            try
            {
                var annualLeaveRequestCreated = _annualLeaveRequestLogic.Create(createAnnualLeaveRequestCRUDModel);

                if (annualLeaveRequestCreated != null && annualLeaveRequestCreated.Year == createAnnualLeaveRequestCRUDModel.Year
                        && string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
                {
                    return CreatedAtAction(nameof(Create),
                        new { id = annualLeaveRequestCreated.AnnualLeaveRequestID }, annualLeaveRequestCreated);
                }
                else
                {
                    if (annualLeaveRequestCreated == null || annualLeaveRequestCreated.Year != createAnnualLeaveRequestCRUDModel.Year)
                    {
                        return BadRequest($"Annual Leave Request was not created");
                    }
                    else if (!string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
                    {
                        return BadRequest($"Annual Leave Request was not created.  Error Messages: {annualLeaveRequestCreated.ErrorMessage}");
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
        public IActionResult Update(AnnualLeaveRequestCRUDModel updateAnnualLeaveRequestCRUDModel)
        {
            try
            {
                var annualLeaveRequestUpdated = _annualLeaveRequestLogic.Update(updateAnnualLeaveRequestCRUDModel);

                if (annualLeaveRequestUpdated != null && annualLeaveRequestUpdated.Year == updateAnnualLeaveRequestCRUDModel.Year 
                    && string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
                {
                    return Ok();
                }
                else
                {
                    if (annualLeaveRequestUpdated == null || annualLeaveRequestUpdated.Year != updateAnnualLeaveRequestCRUDModel.Year)
                    {
                        return BadRequest($"Annual Leave Request was not updated");
                    }
                    else if (!string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
                    {
                        return BadRequest($"Annual Leave Request was not updated.  Error Messages: {annualLeaveRequestUpdated.ErrorMessage}");
                    }
                    else
                    {
                        return BadRequest($"Annual leave request was not updated");
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating the annual leave request");
            }
        }

        [HttpDelete("{annualLeaveRequestID}")]
        public IActionResult Delete(int annualLeaveRequestID)
        {
            try
            {
                _annualLeaveRequestLogic.Delete(annualLeaveRequestID);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error deleting the annual leave request");
            }
        }
    }
}
