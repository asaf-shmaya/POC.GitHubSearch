using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC.GitHubSearch.Services.Models
{
    public partial class RepositoryItemMin : BaseEntity
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }

    }
}