using System.Data;
using System.Web.Http;
using TimeTable_api.Enum;
using TimeTable_api.Models;
using TimeTable_api.Repository;

namespace TimeTable_api.Controllers
{
    /// <summary>
    /// student controller
    /// </summary>
    [JwtAuthorize(Roles = "admin")]
    [RoutePrefix("api/Student")]

    public class StudentController : ApiController
    {
        #region PrivateMembers 

        /// <summary>
        /// private variable of repo
        /// </summary>
        private readonly StudentRepository _repository;

        #endregion


        #region Constructors 

        /// <summary>
        /// student controller's constructor
        /// </summary>
        public StudentController()
        {
            _repository = new StudentRepository();
        }

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return all student list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new { data = _repository.GetAllStudents() });
        }

        /// <summary>
        /// add new student
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(StudentAddDTO studentDTO)
        {
            // convert dto to poco
            _repository.Presave(studentDTO);

            // validate poco
            if(!_repository.Validate()) return BadRequest();

            // save poco
            return Ok(new { data = _repository.Save() });
        }


        /// <summary>
        /// Update the student
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(StudentAddDTO studentDTO)
        {
            _repository.operation = EnumOperation.U;

            // convert dto to poco
            _repository.Presave(studentDTO);

            // validate poco
            if (!_repository.Validate()) return BadRequest();

            // save poco
            return Ok(new { data = _repository.Save() });
        }


        /// <summary>
        /// delete student form database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!_repository.PreDeleteValidate(id)) return BadRequest();

            return Ok(new {data = _repository.Delete(id)});
        }
        #endregion

    }
}
