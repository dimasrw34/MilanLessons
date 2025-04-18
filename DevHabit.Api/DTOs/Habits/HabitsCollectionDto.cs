using DevHabit.Api.DTOs.Common;

namespace DevHabit.Api.DTOs.Habits;

public sealed record HabitsCollectionDto : ICollectionResponse<HabitDto>
{
    public required List<HabitDto> Items { get; init; }
}