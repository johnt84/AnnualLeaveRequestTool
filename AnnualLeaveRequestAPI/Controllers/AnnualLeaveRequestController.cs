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

        [HttpGet("GetYears")]
        public IActionResult GetYears()
        {
            try
            {
                var years = _annualLeaveRequestLogic.GetYears();

                if (years != null && years.Count > 0)
                {
                    return Ok(years);
                }
                else
                {
                    return BadRequest($"Could not retrieve any years");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving years");
            }
        }

        [HttpGet("GetRequestsForYear/{year:int}")]
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

        [HttpGet("Get/{annualLeaveRequestID:int}")]
        public IActionResult Get(int annualLeaveRequestID)
        {
            try
            {
                var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestID);

                if (annualLeaveRequest != null && annualLeaveRequest.Year > 0)
                {
                    return Ok(annualLeaveRequest);
                }
                else
                {
                    return BadRequest($"No annual leave request exists for annualLeaveRequestID: {annualLeaveRequestID}");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving the annual leave request for annualLeaveRequestID: {annualLeaveRequestID}");
            }
        }

        [HttpGet("GetDaysBetweenStartDateAndEndDate/{startDate:DateTime}/{returnDate:DateTime}")]
        public IActionResult GetDaysBetweenStartDateAndEndDate(DateTime startDate, DateTime returnDate)
        {
            try
            {
                decimal daysBetweenStartDateAndReturnDate = _annualLeaveRequestLogic.GetDaysBetweenStartDateAndReturnDate(startDate, returnDate);

                if (daysBetweenStartDateAndReturnDate > 0)
                {
                    return Ok(daysBetweenStartDateAndReturnDate);
                }
                else
                {
                    return BadRequest($"Could not calculate number of days between {startDate} and {returnDate}");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error calculating number of days between {startDate} and {returnDate}");
            }
        }

        [HttpPost]
        public IActionResult Create(AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel)
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
        public ActionResult<AnnualLeaveRequestCRUDModel> Update(AnnualLeaveRequestCRUDModel updateAnnualLeaveRequestCRUDModel)
        {
            try
            {
                var annualLeaveRequestUpdated = _annualLeaveRequestLogic.Update(updateAnnualLeaveRequestCRUDModel);

                if (annualLeaveRequestUpdated != null && annualLeaveRequestUpdated.Year == updateAnnualLeaveRequestCRUDModel.Year 
                    && string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
                {
                    return Ok(annualLeaveRequestUpdated);
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
