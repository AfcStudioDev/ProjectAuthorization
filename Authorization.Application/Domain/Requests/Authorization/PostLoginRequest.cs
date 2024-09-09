using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Application.Domain.Responses.Authorization;
using MediatR;

namespace Authorization.Application.Domain.Requests.Authorization
{
	public class PostLoginRequest : IRequest<PostLoginResponse>
	{
		public string Identificator { get; set; }
		public string Password { get; set; }
	}
}
