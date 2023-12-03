using FluentValidation;
using SmartHomeApi.Contracts.Models.Rooms;

namespace SmartHomeApi.Contracts.Validation
{
    public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
    {
        public AddRoomRequestValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.Voltage).NotEmpty();
            RuleFor(x => x.GasConnected).NotNull();
        }
    }
}
