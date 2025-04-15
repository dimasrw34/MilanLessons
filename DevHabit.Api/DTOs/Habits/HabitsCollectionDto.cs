namespace DevHabit.Api.DTOs.Habits;

public sealed record HabitsCollectionDto
{
    public List<HabitDto> Data { get; init; }
}