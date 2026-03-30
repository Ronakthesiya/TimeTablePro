using System.Web.Http;
using TimeTable_api.Models.DTO;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// slot controller
    /// </summary>
    [RoutePrefix("api/Slot")]

    public class SlotController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        /// private variable of repo
        /// </summary>
        private readonly SlotRepository _repository;

        #endregion

        #region Constructors 
        /// <summary>
        /// constructor of this class
        /// </summary>
        public SlotController()
        {
            _repository = new SlotRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return list opf all slots
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthorize(Roles = "admin")]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllSlots() });
        }

        /// <summary>
        /// return slots of specific branchid
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthorize]
        [Route("branch/{branchId:int}")]
        public IHttpActionResult GetByBranchId(int branchId)
        {
            return Ok(new { data = _repository.GetSlotsBranchId(branchId) });
        }

        //{
        //  "branchId": 1,
        //  "slotNumber": 1,
        //  "dayOfWeek": "Mon",
        //  "timeStart": "09:00",
        //  "timeEnd": "10:00",
        //  "subjectId": 1,
        //  "facultyId": 1,
        //  "placeId": 1
        //}


        /// <summary>
        /// create a new slot
        /// </summary>
        /// <param name="slots"></param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthorize(Roles = "admin")]
        public IHttpActionResult Post(SlotAddDTO[] slots)
        {
            int[] ids = new int[slots.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                // convert dto to poco
                _repository.Presave(slots[i]);

                // validate poco
                if(!_repository.Validate()) return BadRequest();

                // save poco
                ids[i] = _repository.Save();
            }

            return Ok(new { data = ids });
        }

        #endregion

    }
}
