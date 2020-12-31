using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace POC.GitHubSearch.Web.Controllers
{
    public class BookmarksController : ApiController
    {
        HttpSessionState CurrentSession { get => HttpContext.Current.Session; }

        // GET api/<controller>
        [System.Web.Http.Route("api/bookmarks")]
        [System.Web.Http.HttpGet]
        public IEnumerable<int> GetBookmarkList()
        {
            List<int> returnValue = new List<int>();

            if (CurrentSession["bookmarkList"] != null)
            {
                returnValue = (List<int>)CurrentSession["bookmarkList"];
            }

            return returnValue;
        }

        // GET api/<controller>/5
        [System.Web.Http.Route("api/bookmarks/{id}")]
        [System.Web.Http.HttpPost]
        public string Set(int id)
        {
            if (CurrentSession["bookmarkList"] == null)
            {
                CurrentSession["bookmarkList"] = new List<int>() { id };
            }
            else
            {
                List<int> bookmarkList = (List<int>)CurrentSession["bookmarkList"];
                if (!bookmarkList.Contains(id))
                {
                    bookmarkList.Add(id);
                }
                CurrentSession["bookmarkList"] = bookmarkList;
            }

            return "Ok";
        }

        // DELETE api/<controller>/5
        [System.Web.Http.Route("api/bookmarks/{id}")]
        public void Delete(int id)
        {
            if (CurrentSession["bookmarkList"] != null)
            {
                List<int> bookmarkList = (List<int>)CurrentSession["bookmarkList"];
                if (bookmarkList.Contains(id))
                {
                    bookmarkList.Remove(id);
                }
                CurrentSession["bookmarkList"] = bookmarkList;
            }
        }
    }
}