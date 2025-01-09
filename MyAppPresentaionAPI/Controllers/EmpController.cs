using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppApplication.Commands;
using MyAppApplication.DTOs;
using MyAppApplication.Queries;
using MyAppDomain.Entities;
using MyAppDomain.Helper;
using static MyAppApplication.Commands.DeleteEmployeeCommandHandller;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyAppPresentaionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddEmpAsync([FromBody] PostEmpDto empDto, [FromQuery] string? lang = "en")
        {
            var localizer = HttpContext.RequestServices.GetService<LocalizationService>();

            if (empDto == null)
            {
                return BadRequest(new
                {
                    Code = "400",
                    Message = localizer.GetLocalizedString("InvalidRequest", lang)
                });
            }

            var result = await sender.Send(new AddEmpCommand(empDto));

            return result.Code switch
            {
                "200" => Ok(new
                {
                    result.Code,
                    Message = localizer.GetLocalizedString("EmployeeAddedSuccess", lang),
                    result.Data
                }),
                "422" => UnprocessableEntity(new
                {
                    result.Code,
                    Message = localizer.GetLocalizedString("ValidationErrorOccurred", lang),
                    result.ValidationError
                }),
                "400" => BadRequest(new
                {
                    result.Code,
                    Message = localizer.GetLocalizedString("InvalidRequest", lang),
                    result.Error
                }),
                _ => StatusCode(500, new
                {
                    result.Code,
                    Message = localizer.GetLocalizedString("InternalServerError", lang),
                    result.Error
                })
            };
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetEmpDto>>>> GetAllEmpsAsync()
        {
            try
            {
                var serviceResponse = await sender.Send(new GetAllEmpsQuery());
                if (serviceResponse.Data != null)
                {
                    serviceResponse.Code = "200";
                    serviceResponse.Message = "Data returned successfully";
                    return Ok(serviceResponse);
                }
                else
                {
                    return BadRequest(serviceResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse<List<GetEmpDto>>
                {
                    Code = "500",
                    Data = null,
                    Message = "An error occurred",
                    Error = ex.Message,
                    // ValidationError = "",
                    //Error = ex.StackTrace,
                });
            }
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] Guid employeeId, [FromBody] PutEmpDto empDto)
        {
            var result = await sender.Send(new UpdateEmployeeCommand(employeeId, empDto));
            return Ok(result);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] Guid employeeId)
        {
            var result = await sender.Send(new DeleteEmpComm(employeeId));
            return Ok(result);
        }
    }
}