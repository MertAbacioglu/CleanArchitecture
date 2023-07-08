﻿using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveAllocationsController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        List<LeaveAllocationDto> leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }

    // GET api/<LeaveAllocationsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        LeaveAllocationDetailsDto leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailQuery { Id = id });
        return Ok(leaveAllocation);
    }

    // POST api/<LeaveAllocationsController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        Unit response = await _mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveAllocationsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomProblemDetail), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        DeleteLeaveAllocationCommand command = new(id);
        await _mediator.Send(command);
        return NoContent();
    }
}