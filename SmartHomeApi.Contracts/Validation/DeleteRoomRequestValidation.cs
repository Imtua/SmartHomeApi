using FluentValidation;
using SmartHomeApi.Contracts.Models.Rooms;

namespace SmartHomeApi.Contracts.Validation
{
    public class DeleteRoomRequestValidation : AbstractValidator<DeleteRoomRequest>
    {
        public DeleteRoomRequestValidation()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Area).NotEmpty();
            RuleFor(r => r.GasConnected).NotNull();
            RuleFor(r => r.Voltage).NotNull();
        }
    }
}
