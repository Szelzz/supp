using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class GetModel : PageModel
    {
        private readonly IPostRepository postRepository;

        public GetModel(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public Post Post { get; set; }

        public void OnGet(int id)
        {
            Post = postRepository.Get(id);
        }
    }
}
