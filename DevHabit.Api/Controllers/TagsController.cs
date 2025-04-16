using DevHabit.Api.Database;
using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.DTOs.Tags.CREATE;
using DevHabit.Api.DTOs.Tags.GET;
using DevHabit.Api.DTOs.Tags.UPDATE;
using DevHabit.Api.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Controllers;

[ApiController]
[Route("tags")]
public sealed class TagsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TagsCollectionDto>> GetTags()
    {
        List<TagDto> tags = await dbContext.Tags
            .Select(TagQueries.ProjectToDto())
            .ToListAsync();

        var tagsCollectionDto = new TagsCollectionDto
        {
            Data = tags
        };

        return Ok(tagsCollectionDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(string id)
    {
        TagDto? tag = await dbContext
            .Tags
            .Where(t => t.Id == id)
            .Select(TagQueries.ProjectToDto())
            .FirstOrDefaultAsync();

        if (tag is null)
        {
            return NotFound();
        }

        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto createTagDto, IValidator<CreateTagDto> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(createTagDto);

        if (!validationResult.IsValid)
        {
            //return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            //RIGHT TRUE VERSION
            ProblemDetails problem = ProblemDetailsFactory.CreateProblemDetails(
                HttpContext, 
                StatusCodes.Status400BadRequest);
            problem.Extensions.Add("errors", validationResult.ToDictionary());

            return BadRequest(problem);
        }

        Tag tag = createTagDto.ToEntity();

        if (await dbContext.Tags.AnyAsync(t => t.Name == tag.Name))
        {
            //return Conflict($"The tag '{tag.Name}' already exist");
            
            //RIGHT TRUE VERSION
            return Problem(
                detail: $"The tag '{tag.Name}' already exist",
                statusCode: StatusCodes.Status409Conflict);
        }

        dbContext.Tags.Add(tag);
        dbContext.SaveChangesAsync();

        TagDto tagDto = tag.ToDto();

        return CreatedAtAction(nameof(GetTag), new { id = tagDto.Id }, tagDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTag(string id, UpdateTagDto updateTagDto)
    {
        Tag? tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null)
        {
            return NotFound();
        }
        tag.UpdateFromDto(updateTagDto);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(string id)
    {
        Tag? tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        if (tag is null)
        {
            return NotFound();
        }

        dbContext.Tags.Remove(tag);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}