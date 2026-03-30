using System.Web;
using System.Web.Http;
using TimeTable_api.Models;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// branch controller
    /// </summary>
    [RoutePrefix("api/Branch")]
    public class BranchController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        /// branch repository private variable
        /// </summary>
        private readonly BranchRepository _repository;

        #endregion

        #region Constructors 
        /// <summary>
        /// branch controller constrictor to insialize branch repository
        /// </summary>
        public BranchController()
        {
            _repository = new BranchRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return list of all branch
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthorize(Roles = "admin")]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllBranchs() });
        }

        /// <summary>
        /// return branch from id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{branchId:int}")]
        [JwtAuthorize]
        public IHttpActionResult Get(int branchId)
        {
            return Ok(new { data = _repository.GetBranchById(branchId) });
        }

        /// <summary>
        /// create a new branch
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthorize(Roles = "admin")]
        public IHttpActionResult Post(BranchDTO branch)
        {
            // presave convert DTO to POCO
            _repository.Presave(branch);

            // validate the POCO
            if (!_repository.Validate()) return BadRequest();

            // save POCO in database
            branch.Id = _repository.Save();

            return Ok(new { data = branch });
        }

        #endregion

    }
}
