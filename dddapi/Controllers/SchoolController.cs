using dddapi.Models;
using dddapi.ServiceLibrary.Contracts.Contracts;
using dddapi.ServiceLibrary.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dddapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ILogger<SchoolController> _logger;

        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService, ILogger<SchoolController> logger)
        {
            _schoolService = schoolService;
            _logger = logger;
        }

        [HttpGet("getteachers")]
        public ActionResult<List<TeacherDto>> GetTeachers()
        {
            _logger.LogWarning("Method GetTeachers invoked.");
            List<TeacherDto> teachers = _schoolService.GetTeachersDto();
            return Ok(teachers);
        }

        [HttpGet("getteachersbyorder")]
        public ActionResult<List<TeacherDto>> GetTeachersByOrder()
        {
            _logger.LogWarning("Method GetTeachersByOrder invoked.");

            List<TeacherDto> teachers = _schoolService.GetTeachersDtoByOrder();

            return Ok(teachers);
        }

        [HttpPut("updateteachercorrectprefix")]
        public ActionResult<List<TeacherDto>> UpdateTeachersDtoCorrectPrefix(string country)
        {
            _logger.LogWarning("Method UpdateTeachersDtoCorrectPrefix invoked.");

            List<TeacherDto> teachers = _schoolService.UpdateTeachersDtoCorrectPrefix(country);

            return Ok(teachers);
        }

        [HttpGet("getnumberteachers")]
        public ActionResult<List<TeacherDto>> GetNumberTeacher(char digit)
        {
            _logger.LogWarning("Method GetNumberTeacher invoked.");

            List<TeacherDto> teachers = _schoolService.GetNumberTeacher(digit);
            return Ok(teachers);
        }

        [HttpGet("getretiredteachers")]
        public ActionResult<List<IsRetiredTeacher>> GetRetiredTeachers(string country)
        {
            _logger.LogWarning("Method GetRetiredTeachers invoked.");

            List<IsRetiredTeacher> retiredteachers = _schoolService.GetRetiredTeacher(country);
            return Ok(retiredteachers);
        }

        [HttpDelete("deleteteacher")]
        public ActionResult DeleteTeacher(string firstName)
        {
            _logger.LogWarning("Method DeleteTeacher invoked.");

            bool deletedTeacher = _schoolService.DeleteTeacherDto(firstName);
            //var existingStudent = StudentsList.Find(e => e.FirstName == name);
            if (deletedTeacher == true)
            {
                return Ok("Teacher has been deleted.");
            }
            else
            {
                return NotFound("This teacher does not exist.");
            }
        }

        [HttpPut("updateteacher")]
        public ActionResult UpdateTeacher(string firstName, TeacherPresentationModel teacherpresentation) // paso el estudiante-input
        {
            _logger.LogWarning("Method UpdateTeacher invoked.");

            TeacherDto teacherdto = new TeacherDto();
            teacherdto.FirstName = teacherpresentation.FirstName;
            teacherdto.LastName = teacherpresentation.LastName;
            teacherdto.Age = teacherpresentation.Age;
            teacherdto.Telephone = teacherpresentation.Telephone;
            teacherdto.Country = teacherpresentation.Country;
            teacherdto.Course = teacherpresentation.Course;
            teacherdto.Retired = teacherpresentation.Retired;
            TeacherDto teacherUpdatedto = _schoolService.UpdateTeacherDto(firstName, teacherdto); // llamo al metodo de la capa 2_ y le paso estudiante de capa 2_ con valores del estudiante-input

            if (teacherUpdatedto.FirstName == null)
            {
                return BadRequest("Error.");
            }
            else
            {
                return Ok("Student updated.");
            }
        }

        [HttpPost("addteacher")]
        public ActionResult AddTeacher(TeacherPresentationModel teacherpresentation) // aqui metemos student sin email y el email solo se anade con el get
        {
            _logger.LogWarning("Method AddTeacher invoked.");

            TeacherDto teacherdto = new TeacherDto();
            teacherdto.FirstName = teacherpresentation.FirstName;
            teacherdto.LastName = teacherpresentation.LastName;
            teacherdto.Age = teacherpresentation.Age;
            teacherdto.Telephone = teacherpresentation.Telephone;
            teacherdto.Country = teacherpresentation.Country;
            teacherdto.Course = teacherpresentation.Course;
            teacherdto.Retired = teacherpresentation.Retired;
            bool studentStatus = _schoolService.AddTeacherDto(teacherdto);
            if (studentStatus == true)
            {
                return Ok("Teacher added.");
            }
            else
            {
                return BadRequest("Error.");
            }
        }

    [HttpGet("getstudentswithemail")]
        public ActionResult<List<StudentDto>> GetStudentsWithEmail()
        {
            List<StudentDto> students = _schoolService.GetStudentWithEmail();
            return Ok(students);
        }

        [HttpGet("getvalidstudent")]
        public ActionResult<ValidStudentDto> GetValidStudent(string firstName)
        {
            ValidStudentDto validStudent = _schoolService.GetValidStudent(firstName);
            if(validStudent.FirstName == null)
            {
                return BadRequest("Error.");
            }
            else
            {
                return Ok(validStudent);
            }
        }

        [HttpDelete("deletestudent")]
        public ActionResult DeleteStudent(string firstName)
        {
            string deletedStudent = _schoolService.DeleteStudentDto(firstName);
            //var existingStudent = StudentsList.Find(e => e.FirstName == name);
            if (deletedStudent == "Ok")
            {
                return Ok("Student has been deleted.");
            }
            else
            {
                return NotFound("This student does not exist.");
            }
        }


        [HttpPut("updatestudent")]
        public ActionResult UpdateStudent(string firstName, StudentPresentationModel studentUpdatepresentation) // paso el estudiante-input
        {
            StudentDto studentdto = new StudentDto(); // creo nuevo estudiante de la capa 2_
            studentdto.FirstName = studentUpdatepresentation.FirstName; // estudiante-capa 2_ == estudiante-input
            studentdto.LastName = studentUpdatepresentation.LastName;
            studentdto.Age = studentUpdatepresentation.Age;
            StudentDto studentUpdatedto = _schoolService.UpdateStudentDto(firstName, studentdto); // llamo al metodo de la capa 2_ y le paso estudiante de capa 2_ con valores del estudiante-input
           
            if (studentUpdatedto.FirstName == null)
            {
                return BadRequest("Error.");
            }
            else
            {
                return Ok("Student updated.");
            }
        }

        [HttpPost("addstudent")]
        public ActionResult AddStudent(StudentPresentationModel studentpresentation) // aqui metemos student sin email y el email solo se anade con el get
        {
            StudentDto studentdto = new StudentDto();
            studentdto.FirstName = studentpresentation.FirstName;
            studentdto.LastName = studentpresentation.LastName;
            studentdto.Age = studentpresentation.Age;
            string studentStatus = _schoolService.AddStudentDto(studentdto);
            if (studentStatus == "studentadded")
            {
                return Ok("Student added.");
            }
            else
            {
                return BadRequest("Error.");
            }
        }
    }
}
