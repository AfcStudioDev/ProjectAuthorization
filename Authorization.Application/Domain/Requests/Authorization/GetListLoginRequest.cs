using Authorization.Application.Domain.Responses.Authorization;
using Authorization.Application.Domain.Responses.LicenseType;
using MediatR;

namespace Authorization.Application.Domain.Requests.LicenseType
{
    public class GetListLoginRequest : IRequest<GetListLoginResponse>
    {
    }
}
