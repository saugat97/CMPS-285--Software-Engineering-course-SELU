using LionsFeed.Data;
using LionsFeed.Models;
using LionsFeed.ViewModel.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace LionsFeed.Controllers
{

    public class HomeController : BaseController
    {

        private LionsContext _context;
        public HomeController(LionsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //if (User.IsInRole("Admin") || User.IsInRole("User"))
            //{
            //    return RedirectToAction("HomeView");
            //}
            //else

            if (GetLoggedInUser() != null)
                return RedirectToAction(nameof(HomeView));

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult HomeView()
        {
            var allPosts = _context.Posts
                .Include(p => p.Artist);


            var viewModel = new PostViewModel
            {
                Posts = allPosts.OrderByDescending(p => p.CreatedDate),

            };

            var userId = GetLoggedInUser().Id;

            foreach (Post post in viewModel.Posts)
            {
                if (_context.Upvotes.Any(u => u.PostId == post.ID && u.UpvoterId == userId))
                {
                    post.IsLiked = true;
                }
            }

            return View(viewModel);
        }

        //public bool IsPostFlaggedByUser(int PostId)
        //{
        //    var userId = GetLoggedInUser().Id;
        //    var result = false;

        //    result = _context.Flags.Any(f => f.PostId == PostId && f.UserId == userId);

        //    return result;
        //}
    }
}
