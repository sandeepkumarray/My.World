using My.World.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class DocumentViewModel
    {
        public DocumentsModel NewDocModel { get; set; }
        public List<DocumentsModel> Documents { get; set; }
        public List<FoldersModel> Folders { get; set; }
        public List<FoldersModel> ChildFolders { get; set; }
        public int FolderId { get; set; }
        public string FolderTitle { get; set; }

        public DocumentViewModel()
        {
            //Documents = new List<DocumentsModel>();
        }
    }
}
