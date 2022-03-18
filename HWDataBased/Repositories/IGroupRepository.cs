using HWDataBased.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWDataBased.Repositories
{

    interface IGroupRepository
    {
        void AddGroup(Group group);
        Group GetGroupById(int id);
        List<Group> GetAllGroups();
    }
}
