using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class UserPlansViewModel
    {
        public ContentPlansModel UsersPlan { get; set; }
        public List<ContentPlansModel> ContentPlans { get; set; }
    }
}
