using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HWDataBased.Models;

namespace HWDataBased.Repositories
{
    interface IStudentRepository
    {
        void AddStudent(Student student);
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
    }
}
