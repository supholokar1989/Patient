using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Patient.API.Application.CommandService.Command;
using Patient.API.Extensions;
using Patient.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Patient.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            _mediator = mediator;
            
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("app is running...");
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // var result = await _mediator.Send(new GetPatientQuery { Id = id });
            return Ok();
        }
        [HttpPost("CreatePatient")]
        public async Task<IActionResult> Post([FromBody] PatientVisitModel patient)
        {
            
            if (patient.PatientVisit == null)
            {
                return BadRequest();
            }
            var patientVisitCommand = new CreatePatientVisitCommand(patient);
            var createdPatient = await _mediator.Send(patientVisitCommand);
            if (createdPatient)
                return Ok();

            return BadRequest();
        }
    }
}
