using FluentValidation;
using SmartHomeApi.Contracts.Models.Devices;
using SmartHomeApi.Data.Repos;
using System.Linq;

namespace SmartHomeApi.Contracts.Validation
{
    public class AddDeviceRequestValidation : AbstractValidator<AddDeviceRequest>
    {
        private readonly RoomRepository _rooms;
        public AddDeviceRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220);
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.Location).NotEmpty()
                .Must(BeSupported)
                .WithMessage($"Please, choose one of the following locations: {string.Join(',', Values.ValidRooms)}.");
        }

        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(l => l == location);
        }
    }
}
