using dddapi.Infrastructure.Contracts.Models;
using dddapi.ServiceLibrary.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.Infrastructure.Contracts.Contracts
{
    public interface ISchoolRepository
    {
        List<StudentRepository> GetStudents();
        string AddStudent(StudentRepository student);
        StudentRepository GetSingleStudent(string firstName);
        string DeleteStudent(string firstName);
        StudentRepository UpdateStudent(string firstName, StudentRepository student);
        List<TeacherRepository> GetTeachers();
        bool AddTeacher(TeacherRepository teacher);
        bool DeleteTeacher(string name);
        TeacherRepository UpdateTeacher(string name, TeacherRepository teacher);
        List<TeacherRepository> GetTeacherRetired(string country);
        List<TeacherRepository> GetNumberTeacher(char digit);
        List<TeacherRepository> UpdateTeachersCorrectPrefix(string country);
    }
}