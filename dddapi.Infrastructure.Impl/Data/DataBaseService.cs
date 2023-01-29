using dddapi.Infrastructure.Contracts.Models;
using dddapi.Infrastructure.Impl.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace dddapi.Infrastructure.Impl.Data
{
    public class DataBaseService : IDataBaseService // herencia de interfaz
    {
        private readonly ILogger<DataBaseService> _logger;

        private readonly DataContext _dataContext; // injection de dependencia

        public DataBaseService(DataContext studentTeacherContext, ILogger<DataBaseService> logger)
        {
            _dataContext = studentTeacherContext;
            _logger = logger;
        }

        public List<TeacherRepository> GetTeachersRetiredDb(string country)
        {
            try
            {
                List<TeacherRepository> teacherCountryList = _dataContext.Teachers?.Where(t => t.Country == country).ToList();
                return teacherCountryList;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new List<TeacherRepository>();
            }
        }

        //public List<TeacherRepository> UpdateTeachersDbCorrectPrefix(string country)
        //{
        //    try
        //    {
        //        List<TeacherRepository> teachersDbList = _dataContext.Teachers?.Where(t => t.Country == country).ToList();
        //        teachersDbList.ForEach(e =>
        //        {
        //            if (e.Country.ToUpper() == "USA")
        //            {
        //                e.Telephone = "+01" + e.Telephone.Substring(3);
        //            }
        //            else if (e.Country.ToUpper() == "SPAIN")
        //            {
        //                e.Telephone = "+34" + e.Telephone.Substring(3);
        //            }
        //            else if (e.Country.ToUpper() == "ARGENTINA")
        //            {
        //                e.Telephone = "+54" + e.Telephone.Substring(3);
        //            }
        //            else if (e.Country.ToUpper() == "POLAND")
        //            {
        //                e.Telephone = "+48" + e.Telephone.Substring(3);
        //            }
        //        });
        //        _dataContext.SaveChanges();
        //        return teachersDbList;
        //    }
        //    catch
        //    {
        //        return new List<TeacherRepository>();
        //    }
           
        //}

        public List<TeacherRepository> GetTeachersDb()
        {
            try
            {
                return _dataContext.Teachers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);

                return new List<TeacherRepository>();
            }
        }

        public bool AddTeacherDb(TeacherRepository teacher)
        {
            try
            {
                _dataContext.Teachers.Add(teacher);
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);

                return false;
            }
        }

        public bool DeleteTeacherDb(string name)
        {
            try
            {
                TeacherRepository deleteTeacherRepository = _dataContext.Teachers?.Where(e => e.FirstName == name).FirstOrDefault();
                if (deleteTeacherRepository == null)
                {
                    return false;
                }
                else
                {
                    _dataContext.Teachers.Remove(deleteTeacherRepository);
                    _dataContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);

                return false;
            }
        }

        public TeacherRepository UpdateTeacherDb(string firstName, TeacherRepository teacher)
        {
            try
            {
                TeacherRepository updateTeacherRepository = _dataContext.Teachers?.Where(e => e.FirstName == firstName).FirstOrDefault();

                if (updateTeacherRepository == null)
                {
                    return new TeacherRepository();
                }
                else
                {
                    updateTeacherRepository.FirstName = teacher.FirstName;
                    updateTeacherRepository.LastName = teacher.LastName;
                    updateTeacherRepository.Course = teacher.Course;
                    updateTeacherRepository.Telephone = teacher.Telephone;
                    updateTeacherRepository.Age = teacher.Age;
                    updateTeacherRepository.Country = teacher.Country;
                    updateTeacherRepository.Retired = teacher.Retired;
                    _dataContext.SaveChanges();
                    return updateTeacherRepository;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);

                return new TeacherRepository();
            }
        }

        public bool AddStudentDb(StudentRepository student) // anadido proyect references para usar StudentREpository que esta en 2_Domain
        {
            try
            {
                _dataContext.Students.Add(student);
                _dataContext.SaveChanges();
                return true;
            }
            catch // singifica que ha habido un error en try
            {
                return false;
            }
        }

        public List<StudentRepository> GetStudentsDb()
        {
            try
            {
                return _dataContext.Students.ToList();
            }
            catch
            {
                return new List<StudentRepository>();
            }
        }

        public StudentRepository GetSingleStudentDb(string firstName)
        {
            try
            {
                StudentRepository singleStudentRepository = _dataContext.Students?.Where(e => e.FirstName == firstName).FirstOrDefault();
                if (singleStudentRepository == null)
                {
                    return new StudentRepository();
                }
                else
                {
                    return singleStudentRepository;
                }
            }
            catch
            {
                return new StudentRepository();
            }
        }

        public StudentRepository UpdateStudentDb(string firstName, StudentRepository student)
        {
            try
            {
                StudentRepository updateStudentRepository = _dataContext.Students?.Where(e => e.FirstName == firstName).FirstOrDefault();
                if (updateStudentRepository == null)
                {
                    return new StudentRepository();
                }
                else
                {
                    updateStudentRepository.FirstName = student.FirstName;
                    updateStudentRepository.LastName = student.LastName;
                    updateStudentRepository.Age = student.Age;
                    _dataContext.SaveChanges();
                    return updateStudentRepository;
                }
            }
            catch
            {
                return new StudentRepository();
            }
        }

        public string DeleteStudentDb(string firstName)
        {
            try
            {
                StudentRepository deletestudentRepository = _dataContext.Students?.Where(e => e.FirstName == firstName).FirstOrDefault();
                if (deletestudentRepository == null)
                {
                    return "error";
                }
                else
                {
                    _dataContext.Students.Remove(deletestudentRepository);
                    _dataContext.SaveChanges();

                    return "ok";
                }
            }
            catch
            {
                return "error";
            }
        }
    }
}
