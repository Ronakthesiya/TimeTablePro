using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable_api.Models;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// Branch repository BL Logic
    /// </summary>
    public class BranchRepository
    {
        #region PrivateMembers

        private TTC02 _objTTC02;

        #endregion

        #region PublicMethods 

        /// <summary>
        /// return list of branchs from db
        /// </summary>
        /// <returns></returns>
        public List<BranchDTO> GetAllBranchs()
        {
            List<BranchDTO> branches = new List<BranchDTO>();

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                return db.Select<TTC02>().Map(branch => DTOPOCOMapper.Map<TTC02, BranchDTO>(branch));
            }
        }


        /// <summary>
        /// return list of branchs from db
        /// </summary>
        /// <returns></returns>
        public BranchDTO GetBranchById(int branchId)
        {
            TTC02 branch = new TTC02();

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                branch = db.Select<TTC02>(brn => brn.C02F01 == branchId).FirstOrDefault();
            }

            return DTOPOCOMapper.Map<TTC02, BranchDTO>(branch);
        }

        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="branchDTO"></param>
        public void Presave(BranchDTO branchDTO)
        {
            _objTTC02 = DTOPOCOMapper.Map<BranchDTO, TTC02>(branchDTO);
        }



        /// <summary>
        /// Validate branch already exist or not
        /// </summary>
        public bool Validate()
        {
            List<TTC02> branchs = new List<TTC02>();

            // check if branch name is already exists or not
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                branchs = db.Select<TTC02>(brn => brn.C02F02 == _objTTC02.C02F02).ToList();
            }

            return branchs.Count == 0;
        }


        /// <summary>
        /// Insert branch into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int id = 0;

            // use ORM to insert branch
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                id = (int)db.Insert<TTC02>(_objTTC02, selectIdentity: true);
            }

            return id;
        }
        #endregion


    }
}