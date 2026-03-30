using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTable_api.Models.DTO;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// Slot Repository
    /// </summary>
    public class SlotRepository
    {
        #region PrivateMethods

        private TTC07 _objTTC07;

        #endregion

        #region PublicMethods
        /// <summary>
        /// return list of all slots from db
        /// </summary>
        /// <returns></returns>
        public List<TTC07> GetAllSlots()
        {
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                return db.Select<TTC07>();
            }
        }

        /// <summary>
        /// return slots of specific branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public List<TTC07> GetSlotsBranchId(int branchId)
        {
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                return db.LoadSelect<TTC07>(s => s.C07F02 == branchId).OrderBy(s => s.C07F03).ToList();
            }
        }


        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="slotDTO"></param>
        public void Presave(SlotAddDTO slotDTO)
        {
            _objTTC07 = DTOPOCOMapper.Map<SlotAddDTO, TTC07>(slotDTO);
        }



        /// <summary>
        /// Validate branch, faculty, subject, place is exist or not
        /// </summary>
        public Boolean Validate()
        {
            // check if branch id, subject id, faculty id, place id is valid or not
            return _objTTC07.C07F02 > 0
                && _objTTC07.C07F07 > 0
                && _objTTC07.C07F08 > 0
                && _objTTC07.C07F09 > 0;
        }


        /// <summary>
        /// Insert slot into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int id = 0;

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                id = (int)db.Insert<TTC07>(_objTTC07, true);
            }

            return id;
        }

        #endregion
    }
}