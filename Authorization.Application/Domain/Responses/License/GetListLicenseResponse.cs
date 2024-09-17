namespace Authorization.Application.Domain.Responses.License
{
    public class GetListLicenseResponse : BaseResponse
    {
        public List<Entities.License> Licenses { get; set; } = new();
    }
}
