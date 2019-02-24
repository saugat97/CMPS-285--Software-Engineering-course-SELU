using LionsFeed.Data;
using LionsFeed.Models;
using LionsFeed.ViewModel.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LionsFeed.Controllers
{

    public class PostsController : BaseController
    {
        private readonly LionsContext _context;
        private IHostingEnvironment _appHosting;

        public PostsController(LionsContext context, IHostingEnvironment appHosting)
        {
            _context = context;
            _appHosting = appHosting;
        }

        // GET: Posts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var lionsContext = _context.Posts.Include(p => p.Artist);
            return View(await lionsContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Artist)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        [Authorize]
        public IActionResult Create1()
        {
            return View();
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new PostViewModel();
            return View();
        }

        [Authorize]
        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var postContent = model.PostText;

            var user = GetLoggedInUser();

            if (!String.IsNullOrEmpty(postContent))
            {
                var post = new Post
                {

                    ArtistId = user.Id,         //User.IsInRole("Admin")? 1 : 2,//User.Identity.GetUserId()
                    CreatedDate = DateTime.Now,
                    PostText = postContent
                };
                _context.Posts.Add(post);
                _context.SaveChanges();
            }

            return RedirectToAction("HomeView", "Home");

        }
        [Authorize(Roles = "Admin")]
        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", post.ArtistId);
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PostText,CreatedDate,ArtistId,LikeCount")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", post.ArtistId);
            return View(post);
        }
        [Authorize(Roles = "Admin")]
        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Artist)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize(Roles = "Admin")]
        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.ID == id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }


        public IActionResult Like(int id)
        {
            var post = _context.Posts
                .Where(p => p.ID == id).SingleOrDefault();
            post.LikeCount += 1;
            _context.Update(post);
            _context.SaveChanges();

            return RedirectToAction("HomeView", "Home");
        }

        //GET
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetLoggedInUser().Id;

            var files = HttpContext.Request.Form.Files;
            foreach (var image in files)
            {
                if (image != null && image.Length > 0)
                {
                    var file = image;
                    var upload = Path.Combine(_appHosting.WebRootPath, "upload\\post");

                    if (file.Length > 0)
                    {
                        var filename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(upload, filename), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        model.ImageUrl = "/upload/post/" + filename;

                        var post = new Post
                        {
                            ImageUrl = model.ImageUrl,
                            ArtistId = userId,
                            CreatedDate = DateTime.Now
                        };
                        _context.Posts.Add(post);
                        _context.SaveChanges();

                    }
                }
            }
            return RedirectToAction("HomeView","Home");




        }
    }


}
