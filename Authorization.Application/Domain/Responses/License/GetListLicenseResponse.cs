namespace Authorization.Application.Domain.Responses.License
{
    public class GetListDeviceResponse : BaseResponse
    {
        public List<Entities.Device> Devices { get; set; } = new();
    }
}
