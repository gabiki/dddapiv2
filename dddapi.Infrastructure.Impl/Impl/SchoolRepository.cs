using dddapi.Infrastructure.Contracts.Contracts;
using dddapi.Infrastructure.Contracts.Models;
using dddapi.Infrastructure.Impl.Context;
using dddapi.Infrastructure.Impl.Data;
using dddapi.ServiceLibrary.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.Infrastructure.Impl.Impl
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IDataBaseService _dataBaseService;

        public SchoolRepository(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        //1. comprobar que tenemos datos en la cache. Si no hay datos en la cache -> coger los datos de la BD.
        //2. Una vez los hemos cogido de la DB, los metemos en cache. Si no hay datos en la DB -> llamar a la api.
        //3. guardar los datos de la api en DB y despues en la cache
        public List<TeacherRepository> GetTeacherRetired(string country)
        {
            var dbresponse = _dataBaseService.GetTeachersRetiredDb(country);
            if (dbresponse == null)
            {
                return new List<TeacherRepository>();
            }
            else
            {
                return dbresponse;
            }
        }

        public List<TeacherRepository> UpdateTeachersCorrectPrefix(string country)
        {
            List<TeacherRepository> teachersmodified = new List<TeacherRepository>();
            List<TeacherRepository> dbresponse = _dataBaseService.GetTeachersRetiredDb(country);
            if (dbresponse == null)
            {
                return new List<TeacherRepository>();
            }
            else
            {
                dbresponse.ForEach(e =>
                {
                    if (e.Country.ToUpper() == "USA")
                    {
                        e.Telephone = "+01" + e.Telephone.Substring(3);
                    }
                    else if (e.Country.ToUpper() == "SPAIN")
                    {
                        e.Telephone = "+34" + e.Telephone.Substring(3);
                    }
                    else if (e.Country.ToUpper() == "ARGENTINA")
                    {
                        e.Telephone = "+54" + e.Telephone.Substring(3);
                    }
                    else if (e.Country.ToUpper() == "POLAND")
                    {
                        e.Telephone = "+48" + e.Telephone.Substring(3);
                    }

                    TeacherRepository teachermodified = _dataBaseService.UpdateTeacherDb(e.FirstName, e);

                    teachersmodified.Add(teachermodified);
                });
                return teachersmodified;
            }
        }

        public List<TeacherRepository> GetNumberTeacher(char digit)
        {
            var dbresponse = _dataBaseService.GetTeachersDb();
            List<TeacherRepository> teacherDigitList = dbresponse?.Where(t => t.Telephone.Skip(3).Contains(digit)).ToList();

            if (teacherDigitList == null)
            {
                return new List<TeacherRepository>();
            }
            else
            {
                return teacherDigitList;
            }
        }

        public List<TeacherRepository> GetTeachers()
        {
            var dbresponse = _dataBaseService.GetTeachersDb();

            if (dbresponse == null)
            {
                return new List<TeacherRepository>();
            }
            else
            { 
                return dbresponse;
            }
        }

        public bool AddTeacher(TeacherRepository teacher)
        {
            var dbresponse = _dataBaseService.AddTeacherDb(teacher);
            if (dbresponse == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTeacher(string name)
        {
            var dbresponse = _dataBaseService.DeleteTeacherDb(name);
            if (dbresponse == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TeacherRepository UpdateTeacher(string name, TeacherRepository teacher)
        {
            var dbresponse = _dataBaseService.UpdateTeacherDb(name, teacher);
            if (dbresponse.FirstName == null)
            {
                return new TeacherRepository();
            }
            else
            {
                return dbresponse;
            }
        }

        public string AddStudent(StudentRepository student)
        {
            var dbresponse = _dataBaseService.AddStudentDb(student);
            if (dbresponse == true)
            {
                return "ok";
            }
            else
            {
                return "error";
            }
        }

        public List<StudentRepository> GetStudents()
        {
            var dbresponse = _dataBaseService.GetStudentsDb();
            //var infocache = dbresponse

            if (dbresponse.Count == 0)
            {
                return new List<StudentRepository>();
            }
            else
            {
                return dbresponse;
            }
        }

        public StudentRepository GetSingleStudent(string firstName)
        {
            var dbresponse = _dataBaseService.GetSingleStudentDb(firstName);
            //var infocache = dbresponse

            if (dbresponse.FirstName == null)
            {
                return new StudentRepository();
            }
            else
            {
                return dbresponse;
            }
        }

        public StudentRepository UpdateStudent(string firstName, StudentRepository student)
        {
            var dbresponse = _dataBaseService.UpdateStudentDb(firstName, student);
            if (dbresponse.FirstName == null)
            {
                return new StudentRepository();
            }
            else
            {
                return dbresponse;
            }
        }

        public string DeleteStudent(string firstName)
        {
            var dbresponse = _dataBaseService.DeleteStudentDb(firstName);
            if (dbresponse == "ok")
            {
                return "ok";
            }
            else
            {
                return "error";
            }
        }
    }
}



// con el get devolver todo y crear un correo