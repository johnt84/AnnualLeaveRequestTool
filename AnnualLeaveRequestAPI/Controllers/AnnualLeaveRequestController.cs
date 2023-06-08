using AnnualLeaveRequestAPI.Commands;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Models;
using AnnualLeaveRequestAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AnnualLeaveRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnualLeaveRequestController : Controller
    {
        private readonly IMediator _mediator = null;

        public AnnualLeaveRequestController(IAnnualLeaveRequestLogic annualLeaveRequestLogic, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetYears")]
        public async Task<IActionResult> GetYearsAsync()
        {
            try
            {
                var years = await _mediator.Send(new GetYearsQuery());

                if (years != null && years.Count > 0)
                {
                    return Ok(years);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving years");
            }
        }

        [HttpGet("GetRequestsForYear/{year:int}")]
        public async Task<IActionResult> GetRequestsForYearAsync(int year)
        {
            try
            {
                var annualLeaveRequestsForYear = await _mediator.Send(new GetRequestsForYearQuery(year));

                if (annualLeaveRequestsForYear != null && annualLeaveRequestsForYear.Count > 0)
                {
                    return Ok(annualLeaveRequestsForYear);
                }

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving annual leave requests for year: {year}");
            }
        }

        [HttpGet("Get/{annualLeaveRequestID:int}")]
        public async Task<IActionResult> GetAsync(int annualLeaveRequestID)
        {
            try
            {
                var annualLeaveRequest = await _mediator.Send(new GetRequestQuery(annualLeaveRequestID));

                if (annualLeaveRequest != null && annualLeaveRequest.Year > 0)
                {
                    return Ok(annualLeaveRequest);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error retrieving the annual leave request for annualLeaveRequestID: {annualLeaveRequestID}");
            }
        }

        [HttpGet("GetDaysBetweenStartDateAndEndDate/{startDate:DateTime}/{returnDate:DateTime}")]
        public async Task<IActionResult> GetDaysBetweenStartDateAndEndDateAsync(DateTime startDate, DateTime returnDate)
        {
            try
            {
                decimal daysBetweenStartDateAndReturnDate = await _mediator.Send(new GetDaysBetweenStartDateAndReturnDateQuery(startDate, returnDate));

                if (daysBetweenStartDateAndReturnDate > 0)
                {
                    return Ok(daysBetweenStartDateAndReturnDate);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error calculating number of days between {startDate} and {returnDate}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnualLeaveRequestCRUDModel createAnnualLeaveRequestCRUDModel)
        {
            try
            {
                var annualLeaveRequestCreated = await _mediator.Send(new CreateCommand(createAnnualLeaveRequestCRUDModel));

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
                        return UnprocessableEntity("Annual Leave Request was not created");
                    }
                    else if (!string.IsNullOrEmpty(annualLeaveRequestCreated.ErrorMessage))
                    {
                        return UnprocessableEntity($"Annual Leave Request was not created.  Error Messages: {annualLeaveRequestCreated.ErrorMessage}");
                    }
                    else
                    {
                        return UnprocessableEntity("Annual leave request was not created");
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
        public async Task<ActionResult<AnnualLeaveRequestCRUDModel>> UpdateAsync(AnnualLeaveRequestCRUDModel updateAnnualLeaveRequestCRUDModel)
        {
            try
            {
                var annualLeaveRequestUpdated = await _mediator.Send(new UpdateCommand(updateAnnualLeaveRequestCRUDModel));

                if (annualLeaveRequestUpdated != null && annualLeaveRequestUpdated.Year == updateAnnualLeaveRequestCRUDModel.Year 
                    && string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
                {
                    return Ok(annualLeaveRequestUpdated);
                }
                else
                {
                    if (annualLeaveRequestUpdated == null || annualLeaveRequestUpdated.Year != updateAnnualLeaveRequestCRUDModel.Year)
                    {
                        return UnprocessableEntity("Annual Leave Request was not updated");
                    }
                    else if (!string.IsNullOrEmpty(annualLeaveRequestUpdated.ErrorMessage))
                    {
                        return UnprocessableEntity($"Annual Leave Request was not updated.  Error Messages: {annualLeaveRequestUpdated.ErrorMessage}");
                    }
                    else
                    {
                        return UnprocessableEntity("Annual leave request was not updated");
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
        public async Task<IActionResult> DeleteAsync(int annualLeaveRequestID)
        {
            try
            {
                await _mediator.Send(new DeleteCommand(annualLeaveRequestID));

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error deleting the annual leave request");
            }
        }
    }
}
