using System.Web.Http;
using TimeTable_api.Models;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// faculty controller
    /// </summary>
    [JwtAuthorize(Roles = "admin")]
    [RoutePrefix("api/Faculty")]

    public class FacultyController : ApiController
    {
        #region PrivateMembers 
        
        /// <summary>
        /// private variable of faculty repository
        /// </summary>
        private readonly FacultyRepository _repository;

        #endregion

        #region Constructors 
        /// <summary>
        /// constructor of faculty controller 
        /// inisialize the repo
        /// </summary>
        public FacultyController()
        {
            _repository = new FacultyRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return lisat of faculty
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllFacultys() });
        }


        /// <summary>
        /// create a new faculty
        /// </summary>
        /// <param name="facultys"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(FacultyAddDTO[] facultys)
        {
            int[] ids = new int[facultys.Length];

            // trverse to all faculty
            for (int i = 0; i < ids.Length; i++)
            {
                // convert dto to poco
                _repository.Presave(facultys[i]);

                // validate poco
                if(!_repository.Validate()) return BadRequest();

                // save poco
                ids[i] = _repository.Save();
            }

            return Ok(new { data = ids });
        }


        /// <summary>
        /// update the AvailableTime of faculty
        /// </summary>
        /// <param name="faculty"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("AvailableTime")]
        public IHttpActionResult Patch(FacultyAddDTO[] faculty)
        {
            int result = 0;
            foreach (FacultyAddDTO fac in faculty)
            {
                // update availability
                result += _repository.UpdateAvailability(fac.Id, fac.AvailableTime);
            }
            return Ok(new { data = result+" updated"});
        }

        #endregion
    }
}
