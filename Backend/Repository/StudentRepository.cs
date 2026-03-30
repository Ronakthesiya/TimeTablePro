using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using TimeTable_api.Enum;
using TimeTable_api.Models;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Repository
{
    /// <summary>
    /// Student Repository
    /// </summary>
    public class StudentRepository
    {

        #region PrivateMember

        /// <summary>
        /// private object to store Student data for presave, validate, save
        /// </summary>
        private TTC08 _objTTC08;

        #endregion

        #region

        /// <summary>
        /// EnumOperation define the time of operation save method going to perform  Insert,Update 
        /// </summary>
        public EnumOperation operation;

        #endregion

        #region PublicMethods
        /// <summary>
        /// return list of student
        /// </summary>
        /// <returns></returns>
        public List<StudentDTO> GetAllStudents()
        {
            List<TTC08> students = new List<TTC08>();

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                students = db.Select<TTC08>();
            }

            List<StudentDTO> result = new List<StudentDTO>();

            foreach (var student in students)
            {
                StudentDTO cur = GetFullStudentDetails(student);
                result.Add(cur);
            }

            return result;
        }

        /// <summary>
        /// convert DTO to POCO
        /// </summary>
        /// <param name="studentDTO"></param>
        public void Presave(StudentAddDTO studentDTO)
        {
            _objTTC08 = DTOPOCOMapper.Map<StudentAddDTO, TTC08>(studentDTO);
        }



        /// <summary>
        /// Validate branch id exist or not
        /// </summary>
        public bool Validate()
        {
            if (operation == EnumOperation.I)
            {
                return _objTTC08.C08F04 > 0;
            }
            else if(operation == EnumOperation.U)
            {
                if (_objTTC08.C08F01 <= 0) return false;

                int cnt = 0;
                using (IDbConnection db = DatabaseFactory.OpenDbConnection())
                {
                    cnt = (int)db.Count<TTC08>(ttc08 => ttc08.C08F01 == _objTTC08.C08F01);
                }

                if (cnt > 0) return true;

                return false;
            }

            return false;
        }


        /// <summary>
        /// Insert student into database
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (operation == EnumOperation.I)
            {
                int id = 0;

                // use ORM to insert student
                using (var db = DatabaseFactory.OpenDbConnection())
                {
                    id = (int)db.Insert<TTC08>(_objTTC08, selectIdentity: true);
                }

                return id;
            }
            else if(operation == EnumOperation.U)
            {
                using (var db = DatabaseFactory.OpenDbConnection())
                {
                    db.Update<TTC08>(_objTTC08);
                }

                return _objTTC08.C08F01;
            }

            return -1;
        }


        /// <summary>
        /// validate the id before delete
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool PreDeleteValidate(int studentId)
        {
            if (studentId <= 0) return false;

            int cnt = 0;

            using (IDbConnection db = DatabaseFactory.OpenDbConnection())
            {
                cnt = (int)db.Count<TTC08>(ttc08 => ttc08.C08F01 == studentId);
            }

            if (cnt <= 0) return false;

            return true;
        }


        /// <summary>
        /// drop the student wiht given id in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete(int id)
        {
            int val = 0;

            using (IDbConnection db = DatabaseFactory.OpenDbConnection())
            {
                val = db.DeleteById<TTC08>(id);
            }

            if (val > 0) return "Deleted successfuly";

            return "Not Deleted";
        }

        #endregion


        #region PrivateMethods

        /// <summary>
        /// return student details with branch details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentDTO GetFullStudentDetails(TTC08 student)
        {
           StudentDTO studentDTO = DTOPOCOMapper.Map<TTC08, StudentDTO>(student);

            using (var db = DatabaseFactory.OpenDbConnection())
            {
                studentDTO.Branch = db.SelectByIds<TTC02>(new[] { studentDTO.BranchId })[0];
            }

            return studentDTO;
        }
        #endregion
    }
}