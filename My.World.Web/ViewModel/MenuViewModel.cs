using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class MenuViewModel
    {
        public List<MenuItem> DashBoardMenuList { get; set; }
        public List<MenuItem> DefaultPlanMenuList { get; set; }
        public List<MenuItem> PremiumPlanMenuList { get; set; }
        public List<MenuItem> WritingMenuList { get; set; }

        public MenuViewModel()
        {
            DashBoardMenuList = new List<MenuItem>();
            DefaultPlanMenuList = new List<MenuItem>();
            PremiumPlanMenuList = new List<MenuItem>();
            WritingMenuList = new List<MenuItem>();
        }
    }

    public class MenuItem
    {
        public string Header;
        public string CountString;
        public string ItemKey;
        public string Controller;
        public string Action;
        public string Color;
        public string Icon;
    }
}
