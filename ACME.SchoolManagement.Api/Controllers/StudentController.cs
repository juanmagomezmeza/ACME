using Microsoft.AspNetCore.Mvc;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using ACME.SchoolManagement.Core.Domain.Models;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Application.Services.Request;

namespace ACME.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("SchoolManagement")]
    public class StudentController : ControllerBase
    {
        private readonly RequestDispatcher _requestDispatcher;
        //private readonly ILoggerService _logger;


        /// <summary>
        /// inicialize dispatcher and services
        /// </summary>
        /// <param name="requestDispatcher">dispatcher with business logic</param>
        /// <param name="logger">logging exceptions</param>
        /// <exception cref="ArgumentNullException">exception object</exception>
        public StudentController(RequestDispatcher requestDispatcher, ILoggerService logger)
        {
            _requestDispatcher = requestDispatcher ?? throw new ArgumentNullException(nameof(RequestDispatcher));
            //_logger = logger ?? throw new ArgumentNullException(nameof(ILoggerService));
        }

        /// <summary>
        /// Register a student
        /// </summary>
        /// <param name="request">Student info</param>
        /// <returns>Result</returns>
        /// <response code="200">successful</response>
        /// <response code="204">if null</response>  
        /// <response code="400">invalid</response>  
        /// <response code="403">user forbidden</response>  
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentCommand request)
        {
            var result = await _requestDispatcher.Send<RegisterStudentCommand, string>(request);
            return result is null ? NoContent() : Ok(result);
        }


    }
}
