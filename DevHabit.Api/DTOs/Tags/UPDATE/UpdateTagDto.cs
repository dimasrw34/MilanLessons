namespace DevHabit.Api.DTOs.Tags.UPDATE;

public sealed record UpdateTagDto
{
    public required string Name { get; set; }
    public string? Description { get; init; }

}