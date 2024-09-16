namespace Authorization.Application.Domain.Responses.User
{
    public class GetUserResponse : BaseResponse
    {
        public Entities.User User { get; set; } = null!;
    }
}
