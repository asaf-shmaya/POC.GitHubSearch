using POC.GitHubSearch.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace POC.GitHubSearch.Services.Repositories
{
    [RoutePrefix("api/repo")]
    public class RepositoryService : IRepositoryService
    {
        [HttpGet]
        [Route("get")]
        public RepositoriesResultMin GetQuery(string query)
        {
            return new RepositoriesResultMin();
        }
    }
}