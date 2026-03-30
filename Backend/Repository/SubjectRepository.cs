using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using TimeTable_api.Models;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// Subject Repository
    /// </summary>
    public class SubjectRepository
    {
        #region PrivateMember

        private TTC04 _objTTC04;

        #endregion

        #region PublicMethods
        /// <summary>
        ///  return list of all subject
        /// </summary>
        /// <returns></returns>
        public List<SubjectDTO> GetAllSubjects()
        {
            List<TTC04> subjects = new List<TTC04>();

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                subjects = db.Select<TTC04>();
            }

            List<SubjectDTO> result = new List<SubjectDTO>();

            foreach (TTC04 subject in subjects)
            {
                SubjectDTO cur = getFullSubjectDetails(subject);
                result.Add(cur);
            }

            return result;
        }


        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="subjectDTO"></param>
        public void Presave(SubjectAddDTO subjectDTO)
        {
            _objTTC04 = DTOPOCOMapper.Map<SubjectAddDTO, TTC04>(subjectDTO);
        }



        /// <summary>
        /// Validate branch id exist or not
        /// </summary>
        public Boolean Validate()
        {
            return _objTTC04.C04F03 > 0;
        }


        /// <summary>
        /// Insert subject into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int id = 0;

            // use ORM to insert subject
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                id = (int)db.Insert<TTC04>(_objTTC04, selectIdentity: true);
            }

            return id;
        }

        #endregion

        #region PrivateMethod

        /// <summary>
        /// select the full details of branch and faculty of the current subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        private SubjectDTO getFullSubjectDetails(TTC04 subject)
        {
            SubjectDTO subjectDTO = DTOPOCOMapper.Map<TTC04, SubjectDTO>(subject);

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                subjectDTO.Branch = db.SelectByIds<TTC02>(new[] { subjectDTO.BranchId })[0];

                var q = db.From<TTC06>()
                        .Where(x => x.C06F02 == subjectDTO.Id)
                        .Join<TTC03>((a, b) => a.C06F01 == b.C03F01);

                subjectDTO.Facultys = db.Select<TTC03>(q);
            }

            return subjectDTO;
        }

        #endregion
    }
}