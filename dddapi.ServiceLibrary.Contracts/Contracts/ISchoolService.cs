using dddapi.ServiceLibrary.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.ServiceLibrary.Contracts.Contracts
{
    public interface ISchoolService
    {
        List<StudentDto> GetStudentWithEmail();
        string AddStudentDto(StudentDto studentdto);
        ValidStudentDto GetValidStudent(string firstName);
        string DeleteStudentDto(string firstName);
        StudentDto UpdateStudentDto(string firstName, StudentDto studentdto);
        bool AddTeacherDto(TeacherDto teacherdto);
        bool DeleteTeacherDto(string firstName);
        TeacherDto UpdateTeacherDto(string firstName, TeacherDto teacherdto);
        List<TeacherDto> GetTeachersDto();
        List<IsRetiredTeacher> GetRetiredTeacher(string country);
        List<TeacherDto> GetNumberTeacher(char digit);
        List<TeacherDto> GetTeachersDtoByOrder();
        List<TeacherDto> UpdateTeachersDtoCorrectPrefix(string country);
    }
}
