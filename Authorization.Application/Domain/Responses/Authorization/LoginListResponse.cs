using System.Collections.Generic;

namespace Authorization.Application.Domain.Responses.Authorization
{
    public class GetListLoginResponse : BaseResponse
    {
        public List<Entities.User> LoginList { get; set; } = new();
    }
}
