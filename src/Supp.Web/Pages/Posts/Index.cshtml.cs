using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class ListModel : PageModel
    {
        private readonly IPostRepository postRepository;

        public ListModel(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IEnumerable<Post> Posts { get; set; }

        public void OnGet()
        {
            Posts = postRepository.GetAll();
        }
    }
}
