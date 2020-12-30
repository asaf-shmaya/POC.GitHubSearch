using Newtonsoft.Json;
using Octokit;
using POC.GitHubSearch.Services.Models;
using POC.GitHubSearch.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace POC.GitHubSearch.Services.Controllers
{
    [EnableCors(origins: App_Code.Constants.Origins.GitHubSearchWeb, headers: "*", methods: "*")]
    [RoutePrefix("api/repo")]
    public partial class RepositoriesController : ApiController
    {
        #region Fields
        public readonly IRepositoryService _repositoryService;
        #endregion

        #region Ctor
        public RepositoriesController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }
        #endregion

        [HttpGet]
        [Route("search")]
        public async Task<SearchRepositoryResult> GetQueryFull(string query)
        {
            GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("MyOrgnizationName"));

            SearchRepositoriesRequest request = new SearchRepositoriesRequest(query);

            SearchRepositoryResult result = await githubClient.Search.SearchRepo(request);

            return result;
        }

        [HttpGet]
        [Route("search/min")]
        public async Task<RepositoriesResultMin> GetQueryMin(string query)
        {
            GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("MyOrgnizationName"));

            SearchRepositoriesRequest request = new SearchRepositoriesRequest(query);

            SearchRepositoryResult result = await githubClient.Search.SearchRepo(request);

            RepositoriesResultMin resultMin = new RepositoriesResultMin();
            resultMin.RepositoryItems = new List<RepositoryItemMin>();

            if (result.Items != null && result.Items.Count > 0)
            {
                foreach (var item in result.Items)
                {
                    resultMin.RepositoryItems.Add(new RepositoryItemMin() {
                        AvatarUrl = item.Owner.AvatarUrl, 
                        Id = item.Id, 
                        Name = item.Name 
                    });
                }
            }

            return resultMin;
        }
    }
}
