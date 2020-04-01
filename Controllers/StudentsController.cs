using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        

        [HttpGet]
        public IActionResult GetStudents()
        {
            string conString = "Data Source=db-mssql;Initial Catalog=s18580;Integrated Security=True";
            using (var client = new SqlConnection(conString))
            {
                using (var com = new SqlCommand())
                {
                    List<Student> list = new List<Student>();
                com.Connection = client;
                com.CommandText = "SELECT * FROM Student";
                
                com.Open();
                var dr = com.ExecuteReader();
                while(dr.Read)
                {
                        var st = new Student();
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.BirthDate = dr["BirthDate"].ToString();
                        st.Enrollment = dr["IdEnrollment"].ToString();
                        st.IndexNumber = dr["IndexNumber"].ToString();
                        list.Add(st);
                }

                return list;
                }
            
            
            
            }

        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id==1)
            {
                return Ok("Kowalski");
            }else if (id==2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            // add to database
            // generating index number
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult EditStudent(int id)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }
}