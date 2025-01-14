
using Application.KeysAPI.Create;
using Application.KeysAPI.Delete;
using Application.KeysAPI.GetAll;
using Application.KeysAPI.GetById;
using Application.KeysAPI.Seed;
using Application.KeysAPI.Update;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("api/v1/keyapi/")]
public class KeyAPIController : ApiController {

    private readonly ISender mediator;

    public KeyAPIController(ISender mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("CreateKeyAPI")]
    public async Task<IActionResult> CreateKeyAPI([FromBody] CreateKeyAPICommand command){

        var createKeyAPIResult = await mediator.Send(command);

        return createKeyAPIResult.Match(
            keyAPI => Ok(),
            errors => Problem(errors)
        ); 
    }

    [HttpPut("UpdateKeyAPI/{id}")]
    public async Task<IActionResult> UpdateKeyAPI(Guid id, [FromBody] UpdateKeyAPICommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("KeyAPI.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateResult = await mediator.Send(command);

        return updateResult.Match(
            keyAPI => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetKeyAPIById/{id}")]
    public async Task<IActionResult> GetKeyAPIById(Guid id)
    {
        var keyAPIResult = await mediator.Send(new GetKeyAPIByIdQuery(id));

        return keyAPIResult.Match(
            keyAPI => Ok(keyAPI),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllKeyAPI()
    {
        var keyAPIResultLst = await mediator.Send(new GetAllKeyAPIQuery());

        return keyAPIResultLst.Match(
            keyAPI => Ok(keyAPI),
            errors => Problem(errors)
        );
    }

    [HttpDelete("DeleteKeyAPI/{id}")]
    public async Task<IActionResult> DeleteKeyAPI(Guid id)
    {
        var deleteResult = await mediator.Send(new DeleteKeyAPICommand(id));

        return deleteResult.Match(
            deleteAPI => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpPost("SeedKeyAPI")]
    public async Task<IActionResult> SeedKeyAPI([FromBody] SeedKeyAPICommand? command)
    {
        var count = command?.Count ?? 100; 
        var stopwatch = System.Diagnostics.Stopwatch.StartNew(); 

        var seedKeyAPIResult = await mediator.Send(new SeedKeyAPICommand(count));

        stopwatch.Stop();
        
        return seedKeyAPIResult.Match(
            success => Ok(new
            {
                Message = $"{count} KeyAPI records have been seeded successfully.",
                TimeElapsed = $"{stopwatch.ElapsedMilliseconds} ms"
            }),
            errors => Problem(errors)
        );
    }

}