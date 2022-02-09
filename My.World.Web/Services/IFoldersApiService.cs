using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.World.Api.Models;
using System.Web;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
	public interface IFoldersApiService
	{

		Int64 Addfolders(FoldersModel model);

		string Updatefolders(FoldersModel model);

		FoldersModel Getfolders(FoldersModel model);

		string Deletefolders(FoldersModel model);

		List<FoldersModel> GetAllfolders(long userId);
		List<FoldersModel> GetAllChildFolders(long folderId);
        List<FoldersModel> GetEligibleParentFolders(long userid, long folderId);
    }
}
