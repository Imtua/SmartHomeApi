using FluentValidation;

namespace SmartHomeApi.Contracts.Validation
{
    public class DeleteDeviceRequestValidation : AbstractValidator<DeleteDeviceRequest>
    {
        public DeleteDeviceRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
        }
    }
}
