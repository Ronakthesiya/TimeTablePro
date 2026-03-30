using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTable_api.Models;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// Faculty Subject Repository
    /// </summary>
    public class FacultySubjectRepository
    {

        #region PrivateMember

        private TTC06 _objTTC06;

        #endregion

        #region PublicMethods
        /// <summary>
        /// return list of faculty subject from db
        /// </summary>
        /// <returns></returns>
        public List<FacultySubjectDTO> GetAllFacultySubjects()
        {
            List<FacultySubjectDTO> facultySubject = new List<FacultySubjectDTO>();

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                facultySubject = db.Select<TTC06>().ToList().Map(facsub => DTOPOCOMapper.Map<TTC06, FacultySubjectDTO>(facsub));
            }

            return facultySubject;
        }
        
        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="facultysubjectDTO"></param>
        public void Presave(FacultySubjectDTO facultysubjectDTO)
        {
            _objTTC06 = DTOPOCOMapper.Map<FacultySubjectDTO, TTC06>(facultysubjectDTO);
        }



        /// <summary>
        /// Validate faculty, subject id exist or not
        /// </summary>
        public Boolean Validate()
        {
            return _objTTC06.C06F01 > 0 && _objTTC06.C06F02 > 0;
        }


        /// <summary>
        /// Insert facultysubject into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int id = 0;

            // use ORM to insert facultysubject
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                id = (int)db.Insert<TTC06>(_objTTC06);
            }

            return id;
        }
        #endregion
    }
}