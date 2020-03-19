using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
       [HttpGet]
        public string GetStudents()
        {
            return "Kowalski, Malewski, Andrzejewski";
        }

    }
}