using cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski"},
                new Student{IdStudent=2, FirstName="Anna", LastName="Malewski"},
                new Student{IdStudent=3, FirstName="Andrzej", LastName="Andrzejewicz"}
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }


        public bool CheckIndex(string index)
        {
            string conString = "Data Source=db-mssql;Initial Catalog=s18580;Integrated Security=True";
            using (var client = new SqlConnection(conString))
            {
                using (var com = new SqlCommand())
                {            
                    com.Connection = client;
                    com.CommandText = "SELECT * FROM Student WHERE IndexNumber = " + index;

                    com.Open();
                    var dr = com.ExecuteReader();
                    while (dr.Read)
                    {
                        var st = new Student();
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.BirthDate = dr["BirthDate"].ToString();
                        st.Enrollment = dr["IdEnrollment"].ToString();
                        st.IndexNumber = dr["IndexNumber"].ToString();

                        if(st.IndexNumber.Equals(index))
                        {
                            return true;
                        }

                    }

                    return false;
                }



            }
        }
    }
}
