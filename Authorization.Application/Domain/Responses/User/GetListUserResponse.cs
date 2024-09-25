using System.Collections.Generic;

namespace Authorization.Application.Domain.Responses.User
{
    public class GetListUserResponse : BaseResponse
    {
        public List<Entities.User> Users { get; set; } = new();
    }
}
