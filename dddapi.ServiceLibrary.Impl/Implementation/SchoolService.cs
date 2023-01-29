using dddapi.Infrastructure.Contracts.Contracts;
using dddapi.Infrastructure.Contracts.Models;
using dddapi.ServiceLibrary.Contracts.Contracts;
using dddapi.ServiceLibrary.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dddapi.ServiceLibrary.Impl.Implementation
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public List<TeacherDto> GetNumberTeacher(char digit)
        {
            List<TeacherRepository> teachersRepository = _schoolRepository.GetNumberTeacher(digit);
            List<TeacherDto> teachersdto = MapTeachersRepositoryToTeachersDto(teachersRepository);
            if (teachersdto.Count == 0)
            {
                List<TeacherDto> emptyTeacherDto = new List<TeacherDto>(); // si no hay nada, hay que devolver un StudentDto vacio porrque ne el metodo lo pone
                return emptyTeacherDto;
            }
            else
            {
                return teachersdto;
            }
        }

        public List<IsRetiredTeacher> GetRetiredTeacher(string country)
        {
            List<TeacherRepository> teacherRepository = _schoolRepository.GetTeacherRetired(country);
            List<IsRetiredTeacher> isRetiredTeacher = new List<IsRetiredTeacher>();

            for (int i = 0; i < teacherRepository.Count; i++)
            {
                if (teacherRepository[i].Age > 65)
                {
                    IsRetiredTeacher isretired = new IsRetiredTeacher();
                    isretired.FirstName = teacherRepository[i].FirstName;
                    isretired.FirstName = teacherRepository[i].FirstName;
                    isretired.LastName = teacherRepository[i].LastName;
                    isretired.Age = teacherRepository[i].Age;
                    isretired.Retired = teacherRepository[i].Retired;
                    isretired.isRetired = true;
                    isretired.YearsToRetire = 0;
                    isretired.Course = teacherRepository[i].Course;
                    isretired.Telephone = teacherRepository[i].Telephone;
                    isretired.Country = teacherRepository[i].Country;
                    isRetiredTeacher.Add(isretired);
                }
                else
                {
                    IsRetiredTeacher notretired = new IsRetiredTeacher();
                    notretired.FirstName = teacherRepository[i].FirstName;
                    notretired.FirstName = teacherRepository[i].FirstName;
                    notretired.LastName = teacherRepository[i].LastName;
                    notretired.Age = teacherRepository[i].Age;
                    notretired.Retired = teacherRepository[i].Retired;
                    notretired.isRetired = false;
                    notretired.YearsToRetire = (65 - teacherRepository[i].Age);
                    notretired.Course = teacherRepository[i].Course;
                    notretired.Telephone = teacherRepository[i].Telephone;
                    notretired.Country = teacherRepository[i].Country;
                    isRetiredTeacher.Add(notretired);
                }
            }
            return isRetiredTeacher;
        }

        public bool AddTeacherDto(TeacherDto teacherdto) // metemos datos nosotros de arriba hacia abajo, y cambiamos el tipo segun la capa
        {
            TeacherRepository teacherRepository = new TeacherRepository();
            //studentRepository.Id = studentdto.Id;
            teacherRepository.FirstName = teacherdto.FirstName;
            teacherRepository.LastName = teacherdto.LastName;
            teacherRepository.Age = teacherdto.Age;
            teacherRepository.Retired = teacherdto.Retired;
            teacherRepository.Course = teacherdto.Course;
            teacherRepository.Telephone = teacherdto.Telephone;
            teacherRepository.Country = teacherdto.Country;
         
            bool teacheradded = _schoolRepository.AddTeacher(teacherRepository); // se anade y va a database service
            if (teacheradded == true) // comprueba pasando desde database srvice por school repository hasta aqui
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTeacherDto(string firstName)
        {
            bool teacherdeleted = _schoolRepository.DeleteTeacher(firstName);
            if (teacherdeleted == true) // comprueba pasando desde database srvice por school repository hasta aqui
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TeacherDto UpdateTeacherDto(string firstName, TeacherDto teacherdto)
        {
            TeacherRepository teacherRepository = new TeacherRepository();
            teacherRepository.FirstName = teacherdto.FirstName;
            teacherRepository.LastName = teacherdto.LastName;
            teacherRepository.Age = teacherdto.Age;
            teacherRepository.Retired = teacherdto.Retired;
            teacherRepository.Course = teacherdto.Course;
            teacherRepository.Telephone = teacherdto.Telephone;
            teacherRepository.Country = teacherdto.Country;
            TeacherRepository teacherRepos = _schoolRepository.UpdateTeacher(firstName, teacherRepository);
            if (teacherRepos.FirstName == null)
            {
                TeacherDto emptyTeacherdto = new TeacherDto();
                return emptyTeacherdto;
            }
            else
            {
                return teacherdto;
            }
        }

        public List<TeacherDto> GetTeachersDtoByOrder() 
        {
            List<TeacherRepository> teachersRepository = _schoolRepository.GetTeachers();
            List<TeacherDto> teachersdto = MapTeachersRepositoryToTeachersDto(teachersRepository);
            List<char> vowelsList = new List<char>()
            {
                'U',
                'O',
                'I',
                'E',
                'A'
            };

            for (int j = 0; j < vowelsList.Count; j++)
            {
                char vowel = vowelsList[j];
                for (int i = 0; i < teachersdto.Count; i++)
                {
                    if (teachersdto[i].FirstName.StartsWith(vowel)) // [0].Equals(vowel))
                    {
                        //teachersdto.FirstOrDefault(teachersdto[i]);
                        TeacherDto existing = teachersdto[i];
                        teachersdto.RemoveAt(i);
                        teachersdto.Insert(0, existing);
                    }
                    else if (teachersdto[i].LastName.StartsWith(vowel))
                    {
                        //teachersdto.FirstOrDefault(teachersdto[i]);
                        TeacherDto existing = teachersdto[i];
                        teachersdto.RemoveAt(i);
                        teachersdto.Insert((teachersdto.Count), existing);
                        //teachersdto.Insert((teachersdto.Count), teachersdto[i]);
                        //teachersdto.Remove(teachersdto[i]);
                    }
                    else if (teachersdto[i].FirstName.StartsWith(vowel) && (teachersdto[i].LastName.StartsWith(vowel)))
                    {
                        //teachersdto.FirstOrDefault(teachersdto[i]);
                        TeacherDto existing = teachersdto[i];
                        teachersdto.RemoveAt(i);
                        teachersdto.Insert((teachersdto.Count), existing);
                        //teachersdto.Insert((teachersdto.Count), teachersdto[i]);
                        //teachersdto.Remove(teachersdto[i]);
                    }
                    //else if (!(teachersdto[i].FirstName.StartsWith(vowel) && (teachersdto[i].LastName.StartsWith(vowel))))
                    //{
                    //    var x = teachersdto[i].FirstName.StartsWith(vowel);
                    //    List<TeacherDto> value = teachersdto.OrderBy(i => i.FirstName).SkipWhile(x);
                    //}
                }
            };
            return teachersdto;
        }

        public List<TeacherDto> UpdateTeachersDtoCorrectPrefix(string country)
        {
            List<TeacherRepository> teachersRepository = _schoolRepository.UpdateTeachersCorrectPrefix(country);
            List<TeacherDto> teachersdto = MapTeachersRepositoryToTeachersDto(teachersRepository);
            
            return teachersdto;
        }

        public List<TeacherDto> GetTeachersDto() // pasamos datos desde abajo hacia arriba
        {
            List<TeacherRepository> teachersRepository = _schoolRepository.GetTeachers();    
            List<TeacherDto> teachersdto = MapTeachersRepositoryToTeachersDto(teachersRepository);
            if (teachersdto.Count == 0)
            {
                List<TeacherDto> emptyTeacherDto = new List<TeacherDto>(); // si no hay nada, hay que devolver un StudentDto vacio porrque ne el metodo lo pone
                return emptyTeacherDto;
            }
            else
            {
                return teachersdto;
            }
        }

        private List<TeacherDto> MapTeachersRepositoryToTeachersDto(List<TeacherRepository> teachersRepository) // cambiar de List StudentRepository a List StudentDto, se utiliza en getstudents arriba
        {
            List<TeacherDto> teachersDto = new List<TeacherDto>();
            teachersRepository.ForEach(e =>
            {
                TeacherDto teacherDto = new TeacherDto();
                teacherDto.Id = e.Id;
                teacherDto.FirstName = e.FirstName;
                teacherDto.LastName = e.LastName;
                teacherDto.Age = e.Age;
                teacherDto.Course = e.Course;
                teacherDto.Retired = e.Retired;
                teacherDto.Telephone = e.Telephone;
                teacherDto.Country = e.Country;
                teachersDto.Add(teacherDto);
            });
            return teachersDto;
        }

        public string AddStudentDto(StudentDto studentdto) // metemos datos nosotros de arriba hacia abajo, y cambiamos el tipo segun la capa
        {
            StudentRepository studentRepository = new StudentRepository();
            //studentRepository.Id = studentdto.Id;
            studentRepository.FirstName = studentdto.FirstName;
            studentRepository.LastName = studentdto.LastName;
            studentRepository.Age = studentdto.Age;
            
            string studentadded = _schoolRepository.AddStudent(studentRepository); // se anade y va a database service
            if (studentadded == "ok") // comprueba pasando desde database srvice por school repository hasta aqui
            {
                return "studentadded";
            }
            else
            {
                return "studenterror";
            }
        }

        public string DeleteStudentDto(string firstName)
        {
            string studentdeleted = _schoolRepository.DeleteStudent(firstName);
            if (studentdeleted == "ok") // comprueba pasando desde database srvice por school repository hasta aqui
            {
                return "Ok";
            }
            else
            {
                return "Error";
            }
        }

        public StudentDto UpdateStudentDto(string firstName, StudentDto studentdto)
        {
            StudentRepository studentRepository = new StudentRepository();
            studentRepository.FirstName = studentdto.FirstName;
            studentRepository.LastName = studentdto.LastName;
            studentRepository.Age = studentdto.Age;
            StudentRepository studentRepos = _schoolRepository.UpdateStudent(firstName, studentRepository);
            if (studentRepos.FirstName == null)
            {
                StudentDto emptyStudentdto = new StudentDto();
                return emptyStudentdto;
            }
            else
            {
                return studentdto;
            }
        }

        public List<StudentDto> GetStudentWithEmail() // pasamos datos desde abajo hacia arriba
        {
            List<StudentRepository> studentsRepository = _schoolRepository.GetStudents();
            List<StudentDto> studentsDto = MapStudentsRepositoryToStudentsDto(studentsRepository);
            if (studentsDto.Count == 0)
            {
                List<StudentDto> emptyStudentDto = new List<StudentDto>(); // si no hay nada, hay que devolver un StudentDto vacio porrque ne el metodo lo pone
                return emptyStudentDto;
            }
            else
            {
                return studentsDto;
            }
        }

        private List<StudentDto> MapStudentsRepositoryToStudentsDto(List<StudentRepository> studentsRepository) // cambiar de List StudentRepository a List StudentDto, se utiliza en getstudents arriba
        {
            List<StudentDto> studentsDto = new List<StudentDto>();
            studentsRepository.ForEach(e =>
            {
                StudentDto studentDto = new StudentDto();
                studentDto.Id = e.Id;
                studentDto.FirstName = e.FirstName;
                studentDto.LastName = e.LastName;
                studentDto.Age = e.Age;
                studentDto.Email = $"{e.FirstName}.{e.LastName}@juana.com".ToLower();

                studentsDto.Add(studentDto);
            });
            return studentsDto;
        }

        public ValidStudentDto GetValidStudent(string firstName)
        {
            StudentRepository singleStudentRepository = _schoolRepository.GetSingleStudent(firstName);

            ValidStudentDto validStudentDto = new ValidStudentDto();
            validStudentDto.FirstName = singleStudentRepository.FirstName;
            validStudentDto.LastName = singleStudentRepository.LastName;
            validStudentDto.Age = singleStudentRepository.Age;
            validStudentDto.Valid = IsValidStudent(singleStudentRepository);

            if (validStudentDto.FirstName == null)
            {
                ValidStudentDto emptyValidStudentDto = new ValidStudentDto();
                return emptyValidStudentDto;
            }
            else
            {
                return validStudentDto;
            }
        }

        private bool IsValidStudent(StudentRepository singleStudentRepository)
        {
            if (singleStudentRepository.Age > 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
