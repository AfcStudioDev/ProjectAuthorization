using Authorization.Application.Domain.Entities;

namespace Authorization.Application.Domain.Responses.License
{
    public class GetListLicenseResponse : BaseResponse
    {
        public List<Device> Licenses { get; set; } = new();
    }
}
