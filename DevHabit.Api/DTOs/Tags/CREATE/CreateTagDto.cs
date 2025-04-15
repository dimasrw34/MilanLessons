namespace DevHabit.Api.DTOs.Tags.CREATE;

public sealed record CreateTagDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}