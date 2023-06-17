    using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Features.LeaveType.Command.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveTypesController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        List<LeaveTypeDto> leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return Ok(leaveTypes);
    }

    // GET api/<LeaveTypesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        LeaveTypeDetailsDto leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
        return Ok(leaveType);
    }

    // POST api/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType)
    {
        int response = await _mediator.Send(leaveType);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveTypesController>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(UpdateLeaveTypeCommand leaveType)
    {
        await _mediator.Send(leaveType);
        return NoContent();
    }

    // DELETE api/<LeaveTypesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteLeaveTypeCommand command = new DeleteLeaveTypeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}