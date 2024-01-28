using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
      //  private readonly List<Post> listPosts;

        public PostController(AppDbContext c)
        {
            _context = c;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.NextPage = page + 1;
            int dataPerPage = 10;
            int skip = dataPerPage * page - dataPerPage;
            //List<Post> data = GeneratePost();
            List<Post> data = _context.Posts.ToList();
            List<Post> filteredData = data
                //.Where(post => post.id <=10)
                .Skip(skip)
                .Take(dataPerPage)
               .OrderBy(post => post.Id)//urutan dari yang terkecil
                                        //.OrderByDescending(post => post.id)  //urutkan dari yang terbesar

                .ToList();

            return View(filteredData);
        }


        public IActionResult Detail(int id)
        {
            Post data = _context.Posts.Where(Post => Post.Id == id).FirstOrDefault();
            return View(data);
        }

        private List<Post> GeneratePost()
        {
            List<Post> posts = new List<Post>();
            Random random = new Random();
            int id = 1;
            for (int i = 0; i < 100; i++)
            {
                int like = random.Next(0, 100);
                posts.Add(new Post()
                {
                    Id = id,
                    Title = "Judul " + id,
                    Content = "Ini isi artikel",
                    CreatedDate = DateTime.Now,
                    Likes = like,
                });
                id++;
            }
            return posts;
        }

    

    public IActionResult TopLikedPosts()
        {
            List<Post> data = GeneratePost();
            List<Post> topLikedPosts = data.OrderByDescending(post => post.Likes).Take(5).ToList();
            return View(topLikedPosts);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Post data)
        {
            data.Likes = 0;
            data.CreatedDate = DateTime.Now;

            _context.Posts.Add(data);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


}
}

