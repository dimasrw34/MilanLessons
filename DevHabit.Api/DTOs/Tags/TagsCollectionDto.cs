using DevHabit.Api.DTOs.Common;
using DevHabit.Api.DTOs.Tags.GET;

namespace DevHabit.Api.DTOs.Tags;

public sealed record TagsCollectionDto : ICollectionResponse<TagDto>
{
    public required List<TagDto> Items { get; init; }
}