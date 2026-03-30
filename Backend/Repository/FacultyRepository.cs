using Org.BouncyCastle.Utilities;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using TimeTable_api.Models;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// faculty repository
    /// </summary>
    public class FacultyRepository
    {
        #region PrivateMember
        private TTC03 _objTTC03;
        #endregion

        #region PublicMethods
        /// <summary>
        /// return list of all faculty
        /// </summary>
        /// <returns></returns>
        public List<FacultyDTO> GetAllFacultys()
        {
            List<TTC03> facultys = new List<TTC03>();

            using (IDbConnection db = DatabaseFactory.OpenDbConnection())
            {
                facultys = db.Select<TTC03>();
            }

            List<FacultyDTO> result = new List<FacultyDTO>();

            // convert faculty to faculty dto
            foreach (TTC03 faculty in facultys)
            {
                FacultyDTO cur = getFacultyDetails(faculty);

                result.Add(cur);
            }


            return result;
        }


        /// <summary>
        /// update faculty availability in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AvailableTime"></param>
        /// <returns></returns>
        public int UpdateAvailability(int id, string availableTime)
        {
            int updated = 0;

            using (IDbConnection db = DatabaseFactory.OpenDbConnection())
            {
                updated = db.UpdateOnly(
                    () => new TTC03 { C03F04 = availableTime },
                    where: f => f.C03F01 == id
                );
            }

            return updated;
        }


        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="facultyDTO"></param>
        public void Presave(FacultyAddDTO facultyDTO)
        {
            _objTTC03 = DTOPOCOMapper.Map<FacultyAddDTO, TTC03>(facultyDTO);
        }



        /// <summary>
        /// Validate branch is exist or not
        /// </summary>
        public Boolean Validate()
        {
            // check if branch id is valid or not
            return _objTTC03.C03F03 > 0;
        }


        /// <summary>
        /// Insert faculty into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int id = 0;

            using (IDbConnection db = DatabaseFactory.OpenDbConnection())
            {
                id = (int)db.Insert<TTC03>(_objTTC03, true);
            }

            return id;
        }

        #endregion

        #region PrivareMehtods

        /// <summary>
        ///  get the subjects and branch of faculty
        /// </summary>
        /// <param name="faculty"></param>
        /// <returns></returns>
        private FacultyDTO getFacultyDetails(TTC03 faculty)
        {
            FacultyDTO facultyDTO = new FacultyDTO();
            facultyDTO.Id = faculty.C03F01;
            facultyDTO.Name = faculty.C03F02;
            facultyDTO.BranchId = faculty.C03F03;
            char[] arr = faculty.C03F04.ToCharArray();
            facultyDTO.AvailableTime = new bool[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                facultyDTO.AvailableTime[i] = (arr[i] == '1');
            }

            TTC02 branchPoco;
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                 branchPoco = db.SelectByIds<TTC02>(new[] { facultyDTO.BranchId }).FirstOrDefault();

                SqlExpression<TTC06> q = db.From<TTC06>()
                        .Where(x => x.C06F01 == facultyDTO.Id)
                        .Join<TTC04>((a, b) => a.C06F02 == b.C04F01);

                facultyDTO.Subjects = db.Select<TTC04>(q);
            }
                
            facultyDTO.Branch = DTOPOCOMapper.Map<TTC02, BranchDTO>(branchPoco);

            return facultyDTO;
        }

        #endregion
    }
}