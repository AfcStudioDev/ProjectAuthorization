using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Domain.Responses.Authorization
{
	public class PostLoginResponse : BaseResponse
	{
		public string Token { get; set; }
	}
}
