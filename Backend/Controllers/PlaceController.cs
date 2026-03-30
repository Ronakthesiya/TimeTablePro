using System.Web.Http;
using TimeTable_api.Models;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// place controller
    /// </summary>
    [JwtAuthorize(Roles = "admin")]
    [RoutePrefix("api/Place")]

    public class PlaceController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        ///  private variable of place repo
        /// </summary>
        private readonly PlaceRepository _repository;

        #endregion

        #region Constructors 
        /// <summary>
        /// constructor of this class
        /// </summary>
        public PlaceController()
        {
            _repository = new PlaceRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// get list of all places
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllPlaces() });
        }


        /// <summary>
        /// create a new palce
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(PlaceAddDTO place)
        {
            // convert dto to poco
            _repository.Presave(place);

            // validate poco
            if (!_repository.Validate()) return BadRequest();

            // savce poco
            int id = _repository.Save();

            return Ok(new { data = id });
        }

        /// <summary>
        /// update the availabvleTime of places
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("AvailableTime")]
        public IHttpActionResult Patch(PlaceAddDTO[] places)
        {
            int result = 0;
            foreach (PlaceAddDTO place in places)
            {
                // update availability
                result += _repository.UpdateAvailability(place.Id, place.AvailableTime);
            }
            return Ok(new { data = result + " updated" });
        }

        #endregion

    }
}
