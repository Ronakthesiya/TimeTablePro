using Microsoft.IdentityModel.Tokens;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TimeTable_api.Models.DTO;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// auth repository
    /// </summary>
    public class AuthRepository
    {
        #region publicmethod

        /// <summary>
        /// validate user by checking in database
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public LoginDTO ValidateUser(LoginDTO User)
        {
            using (IDbConnection db = DatabaseFactory.OpenDbConnection())
            {

                // check for admin
                TTC01 admin = db.Select<TTC01>(x => x.C01F03 == User.Email
                                            && x.C01F02 == User.Username
                                            && x.C01F04 == User.Password).FirstOrDefault();

                if (admin != null)
                {
                    AssignUserDetails(User, "admin", admin.C01F01, 0);
                    return User;
                }

                //check for faculty
                TTC03 faculty = db.Select<TTC03>(x => x.C03F06 == User.Email
                                            && x.C03F02 == User.Username
                                            && x.C03F05 == User.Password).FirstOrDefault();

                if (faculty != null)
                {
                    AssignUserDetails(User, "Faculty", faculty.C03F01, faculty.C03F03);
                    return User;
                }

                // check for student
                TTC08 student = db.Select<TTC08>(x => x.C08F06 == User.Email
                                            && x.C08F02 == User.Username
                                            && x.C08F05 == User.Password).FirstOrDefault();

                if (student != null)
                {
                    AssignUserDetails(User, "Student", student.C08F01, student.C08F04);
                    return User;
                }
            }

            return null;
        }

        /// <summary>
        /// create and return JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateJWTToken(LoginDTO user)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            byte[] key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWTKey"]);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSettings["JWTIssuer"],
                audience: ConfigurationManager.AppSettings["JWTAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region privatemethod

        private void AssignUserDetails(LoginDTO user, string role, int id, int branchId)
        {
            user.Role = role;
            user.BranchId = branchId;
            user.Id = id;
        }

        #endregion
    }
}