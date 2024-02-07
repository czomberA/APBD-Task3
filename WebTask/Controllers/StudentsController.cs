using Microsoft.AspNetCore.Mvc;
using WebTask.Models;
using WebTask.Sevices;

namespace WebTask.Controllers;
[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IDbService dbService;
    private static HashSet<Student> studentsSet;
    public StudentsController(IDbService dbService)
    {
        this.dbService = dbService;
    }
    
    // GET all students
    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        studentsSet = dbService.AllStudents();
        return Ok(studentsSet);
    }
    
    //GET specific student
     [HttpGet("{ID}")]
        public async Task<IActionResult> GetStudent([FromRoute] string ID)
        {
            studentsSet = dbService.AllStudents();
            Student foundStudent = null;

            foreach (var aStudent in studentsSet)
            {
                if (aStudent.index ==ID)
                {
                    foundStudent = aStudent;
                }
            }

            if (foundStudent != null)
            {
                return Ok(foundStudent);
            }
            return NotFound("! " + ID + " Student not found !");
        }
        
        //EDIT student data
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutStudent([FromRoute] string ID, [FromBody] Student student)
        {
            studentsSet = dbService.AllStudents();
            if (ID is not null)
            {
                var x = studentsSet.FirstOrDefault(w => w.index.Contains(ID));
                if (x != null)
                {
                    studentsSet.Remove(x);
                    dbService.updateStudentSet(studentsSet);
                    student.index = ID;
                    if (dbService.AddStudent(student))
                    {
                        dbService.updateStudentSet(studentsSet);
                        return Created("Edited ",  student); 
                    }
                    dbService.AddStudent(x); 
                    dbService.updateStudentSet(studentsSet);
                    return BadRequest("! Incorrect values !");
                }
                return NotFound("! " + ID + " Student not fount !");
            }

            return BadRequest("Cant be null");
        }
        
        //ADD new student
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            studentsSet = dbService.AllStudents();
            
            if (dbService.AddStudent(student))
            {
                dbService.updateStudentSet(studentsSet);
                return Created("Added > ", student);
            }

            return BadRequest("! Could not add student. Incorrect values. !");
        }

        //DELETE student
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] string ID)
        {
            
            studentsSet = dbService.AllStudents();
            
            if (ID is not null)
            {
                Student toDelete = studentsSet.FirstOrDefault(w => w.index.Contains(ID));

                if (toDelete is not null)
                {
                    studentsSet.Remove(toDelete);
                    dbService.updateStudentSet(studentsSet);
                    return Ok(ID + " -> Student deleted");
                }

                return NotFound("! " + ID + " Student not found !");
            }
            return BadRequest("ID can't be null");
        } 
    
}
