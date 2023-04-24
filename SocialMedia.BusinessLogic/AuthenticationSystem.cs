using SocialMedia.BusinessLogic.Interfaces;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic
{
	public class AuthenticationSystem : IAuthenticationSystem
	{
		private readonly IUserDataAccess _userDataAccess;
		private readonly IPasswordHelper _passwordHelper;

		public AuthenticationSystem(IUserDataAccess userDataAccess, IPasswordHelper passwordHelper)
		{
            _userDataAccess = userDataAccess;
			_passwordHelper = passwordHelper;
        }


        public bool ValidateCredentials(string username, string password) 
		{
			bool isUserValid = false;

			//Get the raw password form users
			//Get the salt stored in the database using username
			//If the salt is null, then username is wrong and return false
			//if present get the salt and hash the given raw password using that salt
			//in database, check if this created password is equal to password at username

			
			var salt = _userDataAccess.GetSalt(username);
			if (salt != null)
			{
				var userEnteredHashedPassword = _passwordHelper.GetHashedPassword(password, salt);

				var passwordFromDataBase = _userDataAccess.GetPassword(username);

				if(userEnteredHashedPassword == passwordFromDataBase)
				{
					isUserValid = true;
				}
			}
			return isUserValid;


		}

	}
}
