using System;
using System.Web;
using System.Web.Http;
using TimeTable_api.Models.DTO;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    public class AuthController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        /// private variable of auth repository
        /// </summary>
        private AuthRepository authRepository;

        #endregion

        #region Constructors 

        /// <summary>
        /// Auth controller insialize the auth repository
        /// </summary>
        public AuthController()
        {
            authRepository = new AuthRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// Validate User and create JWT token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [GlobalExceptionFilter]
        [Route("api/login")]
        
        public IHttpActionResult Login(LoginDTO model)
        {
            //throw new DivideByZeroException();

            // validate user
            LoginDTO user = authRepository.ValidateUser(model);

            // if user not valid
            if (user == null)
                return Unauthorized();

            //create jwt token
            string tokenString = authRepository.CreateJWTToken(user);
            HttpContext.Current.Response.Headers.Add("token", tokenString);

            return Ok(new { token = tokenString, role = user.Role , id = user.Id , bid = user.BranchId});
        }

        #endregion
    }
}
