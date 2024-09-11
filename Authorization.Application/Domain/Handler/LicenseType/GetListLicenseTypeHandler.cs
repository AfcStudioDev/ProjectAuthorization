using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Requests.LicenseType;
using Authorization.Application.Domain.Responses.LicenseType;
using MediatR;

namespace Authorization.Application.Domain.Handler.LicenseType
{
    public class GetListLicenseTypeHandler : IRequestHandler<GetListLicenseTypeRequest, GetListLicenseTypeResponse>
    {
        private readonly IRepository<Entities.LicenseType> _repository;

        public GetListLicenseTypeHandler(IRepository<Entities.LicenseType> repository)
        {
            _repository = repository;
        }

        public async Task<GetListLicenseTypeResponse> Handle(GetListLicenseTypeRequest request, CancellationToken cancellationToken)
        {
            return new GetListLicenseTypeResponse() { LicenseTypes = _repository.Get().ToList(), Success = true };
        }
    }
}
