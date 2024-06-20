using ACME.SchoolManagement.Core.Application.Services.Request;
using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACME.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("SchoolManagement")]
    public class EnrollmentController : ControllerBase
    {
        private readonly RequestDispatcher _requestDispatcher;
        private readonly ILoggerService _logger;

        /// <summary>
        /// inicialize dispatcher and services
        /// </summary>
        /// <param name="requestDispatcher">dispatcher with business logic</param>
        /// <param name="logger">logging exceptions</param>
        /// <exception cref="ArgumentNullException">exception object</exception>
        public EnrollmentController(RequestDispatcher requestDispatcher, ILoggerService logger)
        {
            _requestDispatcher = requestDispatcher ?? throw new ArgumentNullException(nameof(RequestDispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILoggerService));
        }

        /// <summary>
        /// Register a course
        /// </summary>
        /// <param name="request">course info</param>
        /// <returns>Result</returns>
        /// <response code="200">successful</response>
        /// <response code="204">if null</response>  
        /// <response code="400">invalid</response>  
        /// <response code="403">user forbidden</response>  
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [HttpPost("Enrollment")]
        public async Task<IActionResult> ContractCourse([FromBody] ContractCourseCommand request)
        {
            var result = await _requestDispatcher.Send<ContractCourseCommand, string?>(request);
            return result is null ? NoContent() : Ok(result);
        }
    }
}
