using Microsoft.AspNetCore.Mvc;
using ClientSystem.Data;
using ClientSystem.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClientSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ClientSystemContext _context;

        public UserController(ClientSystemContext context)
        {
            _context = context;
        }

        // GET: User/Index (List all users)
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: User/Create (Display the registration form)
        public IActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        // POST: User/Create (Handle user registration)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}