namespace Authorization.Application.Domain.Responses.Authorization
{
    public class PostLoginResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}
