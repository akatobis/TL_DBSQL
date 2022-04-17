using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HWDataBased.Models;

namespace HWDataBased.Repositories
{
    interface IGroupsOfStudents
    {
        void AddStudentInGroup(GroupsOfStudents groupsOfStudents);
        List<GroupsOfStudents> GetStudentAndGroupsById();
        List<GroupsOfStudents> GetAllStudentByGroupId(int groupsId);
    }
}
