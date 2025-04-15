using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;


public static class HabitMappings
{
    public static HabitDto ToDto(this Habit habit)
    {
        return new HabitDto
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            Type = habit.Type,
            Frequency = new FrequencyDto
            {
                Type = habit.Frequency.Type,
                TimesPerPeriod = habit.Frequency.TimesPerPeriod
            },
            Target = new TargetDto
            {
                Value = habit.Target.Value,
                Unit = habit.Target.Unit
            },
            HabitStatus = habit.HabitStatus,
            IsArchived = habit.IsArchived,
            EndDate = habit.EndDate,
            Milestone = habit.Milestone == null
                ? null
                : new MilestoneDto
                {
                    Target = habit.Milestone.Target,
                    Current = habit.Milestone.Current
                },
            CreatedAtUtc = habit.CreatedAtUtc,
            UpdatedAtUtc = habit.UpdatedAtUtc,
            LastComplitedAtUtc = habit.LastComplitedAtUtc
        };
    }

    public static Habit ToEntity(this CreateHabitDto dto)
    {
        Habit habit = new()
        {
            Id = $"h_{Guid.CreateVersion7()}",
            Name = dto.Name,
            Description = dto.Description,
            Type = dto.Type,
            Frequency = new Frequency
            {
                Type = dto.Frequency.Type,
                TimesPerPeriod = dto.Frequency.TimesPerPeriod
            },
            Target = new Target
            {
                Value = dto.Target.Value,
                Unit = dto.Target.Unit
            },
            HabitStatus = HabitStatus.Ongoing,
            IsArchived = false,
            EndDate = dto.EndDate,
            Milestone = dto.Milestone is not null
                ? new Milestone
                {
                    Target = dto.Milestone.Target,
                    Current = 0 // Инициализируем прогресс равным 0
                }
                : null,
            CreatedAtUtc = DateTime.UtcNow
        };
        return habit;
    }

    public static void UpdateFromDto(this Habit habit, UpdateHabitDto dto)
    {
        habit.Name = dto.Name;
        habit.Description = dto.Description;
        habit.Type = dto.Type;
        habit.EndDate = dto.EndDate;

        habit.Frequency = new Frequency
        {
            Type = dto.Frequency.Type,
            TimesPerPeriod = dto.Frequency.TimesPerPeriod
        };

        habit.Target = new Target
        {
            Value = dto.Target.Value,
            Unit = dto.Target.Unit
        };

        if (dto.Milestone is not null)
        {
            habit.Milestone = habit.Milestone ?? new Milestone();
            habit.Milestone.Target = dto.Milestone.Target;
        }

        habit.UpdatedAtUtc = DateTime.UtcNow;
    }
}