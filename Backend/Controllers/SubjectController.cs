using System.Web.Http;
using TimeTable_api.Models;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// subject controller
    /// </summary>    
    [JwtAuthorize(Roles = "admin")]
    [RoutePrefix("api/Subject")]

    public class SubjectController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        /// private variable of repo
        /// </summary>
        private readonly SubjectRepository _repository;

        #endregion


        #region Constructors 
        /// <summary>
        /// constructor of this class
        /// </summary>
        public SubjectController()
        {
            _repository = new SubjectRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return list of all subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllSubjects() });
        }


        /// <summary>
        ///  create new subjects
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(SubjectAddDTO[] sub)
        {
            int[] ids = new int[sub.Length];

            for(int i=0;i<ids.Length; i++)
            {
                // convert dro to poco
                _repository.Presave(sub[i]);

                // validate poco
                if (!_repository.Validate()) return BadRequest();

                // save poco
                ids[i] = _repository.Save();
            }

            return Ok(new { data = ids });
        }

        #endregion

    }
}
