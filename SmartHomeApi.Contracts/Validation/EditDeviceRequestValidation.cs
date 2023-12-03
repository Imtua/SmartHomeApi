using FluentValidation;
using SmartHomeApi.Contracts.Models.Devices;
using System.Linq;

namespace SmartHomeApi.Contracts.Validation
{
    public class EditDeviceRequestValidation : AbstractValidator<EditDeviceRequest>
    {
        public EditDeviceRequestValidation()
        {
            RuleFor(x=> x.NewName).NotEmpty();
            RuleFor(x => x.NewRoom).NotEmpty()
                .Must(BeSuported)
                .WithMessage($"Please, choose one of the following locations: {string.Join(',', Values.ValidRooms)}");
        }

        private bool BeSuported(string location)
        {
            return Values.ValidRooms.Any(l => l == location);
        }
    }
}
