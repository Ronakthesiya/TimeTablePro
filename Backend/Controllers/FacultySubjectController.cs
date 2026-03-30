using System.Web.Http;
using TimeTable_api.Models;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// faculty subject controller
    /// </summary>
    [JwtAuthorize(Roles = "admin")]
    [RoutePrefix("api/FacultySubject")]

    public class FacultySubjectController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        /// private variable of repository of this class
        /// </summary>
        private readonly FacultySubjectRepository _repository;

        #endregion

        #region Constructors 

        /// <summary>
        /// constroctore of this class create object of repo
        /// </summary>
        public FacultySubjectController()
        {
            _repository = new FacultySubjectRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return the list of subject with faculty
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllFacultySubjects() });
        }

        /// <summary>
        /// add new faculty subject connection
        /// </summary>
        /// <param name="facultySubject"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Get(FacultySubjectDTO[] facultySubject)
        {
            for (int i = 0; i < facultySubject.Length; i++)
            {
                // convert dto to poco
                _repository.Presave(facultySubject[i]);

                // validate poco
                if(!_repository.Validate()) return BadRequest();

                _repository.Save();
            }

            return Ok(new { data = "relation added" });
        }

        #endregion


    }
}
