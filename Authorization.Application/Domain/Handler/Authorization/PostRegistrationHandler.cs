using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Application.Abstractions;
using Authorization.Application.AuthorizeOptions;
using Authorization.Application.Domain.Entities;
using Authorization.Application.Domain.Requests.Authorization;
using Authorization.Application.Domain.Responses.Authorization;

namespace Authorization.Application.Domain.Handler.Authorization
{
	public class PostRegistrationHandler
	{
		private readonly IRepository<User> _repository;
		private HashPassword _hasher;

		public PostRegistrationHandler( IRepository<User> repository, HashPassword hasher )
		{
			this._repository = repository;
			_hasher = hasher;
		}

		public async Task<PostRegistrationResponse> Handle( PostRegistrationRequest request, CancellationToken cancellationToken )
		{
			if ( ValidData( request ) )
			{ 
			
			}
			if ( CheckOnExist( request ) )
			{
				return new PostRegistrationResponse() { Success = false, Message = "User Exist" };
			}
			else
			{
				Random rnd = new Random();
				var code = rnd.Next( 0, 999999 ).ToString( "D6" );

				byte[] salt = _hasher.CreateDinamicSaltFromEmail( request.Email );
				User user = new User()
				{
					Id = Guid.NewGuid(),//todo лучше id пусть база содает
					Email = request.Email,									
					PasswordHash = _hasher.EncryptingPass( request.Password, salt )
				};
				
				try
				{
					var countSaveEntities = _repository.Create( user );
					if ( countSaveEntities == 1 )
					{						
						return new PostRegistrationResponse() { Success = true };
					}
					else
					{
						return new PostRegistrationResponse() { Success = false, Message = "Error on add Entity" };
					}
				}
				catch ( Exception e )
				{
					return new PostRegistrationResponse() { Success = false, Message = e.Message };
				}
			}
		}

		private bool CheckOnExist( PostRegistrationRequest request )
		{
			User user = _repository.Get().FirstOrDefault( u => u.Email == request.Email );
			if ( user != null )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool ValidData( PostRegistrationRequest request)
		{

			var x = "^[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$" +
				"";
				return true;
		}
	}
}
