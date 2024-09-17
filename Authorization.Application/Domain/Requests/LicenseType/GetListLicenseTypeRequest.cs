using Authorization.Application.Domain.Responses.LicenseType;
using MediatR;
using Authorization.Application.Domain.Responses.Authorization;

namespace Authorization.Application.Domain.Requests.Authorization
{
    public class GetListLicenseTypeRequest : IRequest<GetListLicenseTypeResponse>
    {
    }
}
