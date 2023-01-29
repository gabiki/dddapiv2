using dddapi.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.Infrastructure.Impl.Data
{
    public interface IDataBaseService
    {
        List<StudentRepository> GetStudentsDb();
        bool AddStudentDb(StudentRepository student);
        StudentRepository GetSingleStudentDb(string firstName);
        string DeleteStudentDb(string firstName);
        StudentRepository UpdateStudentDb(string firstName, StudentRepository student);
        List<TeacherRepository> GetTeachersDb();
        bool AddTeacherDb(TeacherRepository teacher);
        bool DeleteTeacherDb(string name);
        TeacherRepository UpdateTeacherDb(string firstName, TeacherRepository teacher);
        List<TeacherRepository> GetTeachersRetiredDb(string country);
        //List<TeacherRepository> UpdateTeachersDbCorrectPrefix(string country);
    }
}
