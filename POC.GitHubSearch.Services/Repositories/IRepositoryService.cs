using POC.GitHubSearch.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.GitHubSearch.Services.Repositories
{
    public partial interface IRepositoryService
    {
        RepositoriesResultMin GetQuery(string query);
    }
}
