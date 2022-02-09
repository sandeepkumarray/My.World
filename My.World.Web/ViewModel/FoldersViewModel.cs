using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class FoldersViewModel
    {
        public FoldersModel FoldersModel { get; set; }
        public FoldersModel ParentFolder { get; set; }
        public List<FoldersModel> FoldersList { get; set; }


    }
}
