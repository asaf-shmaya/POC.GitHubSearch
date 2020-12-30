using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC.GitHubSearch.Services.Models
{
    public abstract partial class BaseEntity
    {
        public long Id { get; set; }
    }
}