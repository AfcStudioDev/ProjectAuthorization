using Authorization.Application.Domain.Responses.License;

using MediatR;

namespace Authorization.Application.Domain.Requests.License
{
    public class GetListLicenseRequest : IRequest<GetListLicenseResponse>
    {
        public Guid? UserId { get; set; }
    }
}