using Authorization.Application.Domain.Responses.LicenseType;

using MediatR;

namespace Authorization.Application.Domain.Requests.Authorization
{
    public class GetListLicenseTypeRequest : IRequest<GetListLicenseTypeResponse>
    {
    }
}
