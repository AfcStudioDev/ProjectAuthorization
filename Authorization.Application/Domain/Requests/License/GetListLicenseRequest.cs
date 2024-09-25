using Authorization.Application.Domain.Responses.License;
using System;
using MediatR;

namespace Authorization.Application.Domain.Requests.License
{
    public class GetListLicenseRequest : IRequest<GetListLicenseResponse>
    {
        public Guid? UserId { get; set; }
    }
}