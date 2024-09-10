using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.Payment;
using Authorization.Application.Domain.Responses.Payment;
using MediatR;

namespace Authorization.Application.Domain.Handler.Payment
{
    public class MakePaymentAndConfirmHandler : IRequestHandler<MakePaymentAndConfirmRequest, MakePaymentAndConfirmResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Device> _deviceRepository;

        public MakePaymentAndConfirmHandler(IRepository<User> repositoryUser, IRepository<Device> repositoryDevice)
        {
            _userRepository = repositoryUser;
            _deviceRepository = repositoryDevice;
        }

        public async Task<MakePaymentAndConfirmResponse> Handle(MakePaymentAndConfirmRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetWithInclude(a => a.Id == (Guid)request.UserId!, a => a.Devices).First();

            var device = user.Devices.FirstOrDefault(a => a.DeviceNumber == request.DeviceNumber);

            var newLicense = new Entities.License() { LicenseTypeId = request.LicenseType, StartLicense = DateTime.Now };

            if (device != null)
            {
                var licenses = _deviceRepository.GetWithInclude(a => a.Id == device.Id, a => a.Licenses).FirstOrDefault()!.Licenses;

                if (licenses.Count > 0)
                {
                    var endlicense = licenses.OrderByDescending(a => a.StartLicense).First();

                    newLicense.StartLicense = endlicense.StartLicense + endlicense.LicenseType.Length;
                }
                else
                    newLicense.StartLicense = DateTime.Now;

                device.Licenses.Add(newLicense);

                _deviceRepository.Update(device);
            }
            else
            {
                var newDevice = new Device() { DeviceNumber = request.DeviceNumber, LicenseKey = Guid.NewGuid().ToString(), UserId = (Guid)request.UserId! };
                newDevice.Licenses.Add(newLicense);
            }

            return new MakePaymentAndConfirmResponse() { Success = true };
        }
    }
}
