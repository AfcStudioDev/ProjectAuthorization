using System.Collections.Generic;

namespace Authorization.Application.Domain.Responses.LicenseType
{
    public class GetListLicenseTypeResponse : BaseResponse
    {
        public List<Entities.LicenseType> LicenseTypes { get; set; } = new();
    }
}
