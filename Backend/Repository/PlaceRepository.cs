using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TimeTable_api.Models;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// Place Repository
    /// </summary>
    public class PlaceRepository
    {
        #region PrivateMember

        private TTC05 _objTTC05;

        #endregion

        #region PublicMethods
        /// <summary>
        /// get all place from db
        /// </summary>
        /// <returns></returns>
        public List<PlaceDTO> GetAllPlaces()
        {
            List<TTC05> places = new List<TTC05>();

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                places = db.Select<TTC05>();
            }

            List<PlaceDTO> result = new List<PlaceDTO>();

            // covert poco to dto
            foreach (var place in places)
            {
                PlaceDTO cur = GetPlaceDetails(place);
                result.Add(cur);
            }

            return result;
        }

        /// <summary>
        /// update availabilty of place in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AvailableTime"></param>
        /// <returns></returns>
        public int UpdateAvailability(int id, string AvailableTime)
        {
            int updated = 0;

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                updated = db.UpdateOnly(
                    () => new TTC05 { C05F04 = AvailableTime },
                    where: f => f.C05F01 == id
                );
            }

            return updated;
        }


        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="placeDTO"></param>
        public void Presave(PlaceAddDTO placeDTO)
        {
            _objTTC05 = DTOPOCOMapper.Map<PlaceAddDTO, TTC05>(placeDTO);
        }



        /// <summary>
        /// Validate place already exist or not
        /// </summary>
        public Boolean Validate()
        {
            List<TTC05> places = new List<TTC05>();

            // check if place name is already exists or not
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                places = db.Select<TTC05>(brn => brn.C05F02 == _objTTC05.C05F02).ToList();
            }

            return places.Count == 0;
        }


        /// <summary>
        /// Insert place into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int id = 0;

            // use ORM to insert place
            using (var db = DatabaseFactory.OpenDbConnection())
            {
                id = (int)db.Insert<TTC05>(_objTTC05, true);
            }

            return id;
        }
        #endregion

        #region PrivateMethos

        /// <summary>
        /// Convert DTO to Poco with Available time to bool array from string
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        private PlaceDTO GetPlaceDetails(TTC05 place)
        {
            PlaceDTO placeDTO = new PlaceDTO() 
            {
                Id = place.C05F01,
                Name = place.C05F02,
                Type = place.C05F03
            };

            char[] arr = place.C05F04.ToCharArray();
            placeDTO.AvailableTime = new bool[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                placeDTO.AvailableTime[i] = (arr[i] == '1');
            }

            return placeDTO;
        }

        #endregion
    }
}