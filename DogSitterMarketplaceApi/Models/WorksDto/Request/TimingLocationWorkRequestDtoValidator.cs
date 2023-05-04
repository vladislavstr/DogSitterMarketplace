using FluentValidation;

namespace DogSitterMarketplaceApi.Models.WorksDto.Request
{
    public class TimingLocationWorkRequestDtoValidator : AbstractValidator<TimingLocationWorkRequestDto>
    {
        private int _entHour = 24;
        private int _entMinute = 60;
        public TimingLocationWorkRequestDtoValidator()
        {
            RuleFor(t => t.DayOfWeekId).GreaterThan(0);
            RuleFor(t => t.LocationWorkId).GreaterThan(0);
            RuleFor(t => t.StartHour).GreaterThanOrEqualTo(0).LessThan(_entHour);
            RuleFor(t => t.StartMinut).GreaterThanOrEqualTo(0).LessThan(_entMinute);
            RuleFor(t => t.StopHour).GreaterThanOrEqualTo(0).LessThan(_entHour);
            RuleFor(t => t.StopMinut).GreaterThanOrEqualTo(0).LessThan(_entMinute);
        }
    }
}