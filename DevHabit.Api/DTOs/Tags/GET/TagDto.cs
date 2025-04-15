namespace DevHabit.Api.DTOs.Tags.GET;

public sealed record TagDto
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; init; }
    public required DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
}