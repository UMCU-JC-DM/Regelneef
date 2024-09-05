using Microsoft.AspNetCore.Mvc;
using ClientSystem.Data;
using ClientSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSystem.Controllers
{
    public class DatasetRequestController : Controller
    {
        private readonly ClientSystemContext _context;

        public DatasetRequestController(ClientSystemContext context)
        {
            _context = context;
        }
        // GET: DatasetRequest
        public async Task<IActionResult> Index()
        {
            var datasetRequests = await _context.DatasetRequests.Include(dr => dr.RequestedByUser).ToListAsync();
            return View(datasetRequests);
        }

        // GET: DatasetRequest/Create (Display the form)
        public async Task<IActionResult> Create()
        {
            // Load all users for selection
            ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "UserId", "Username");
            var datasetRequest = new DatasetRequest();
            return View(datasetRequest);
        }

        // POST: DatasetRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestedBy,DatasetType")] DatasetRequest datasetRequest)
        {
            if (ModelState.IsValid)
            {
                // Fetch the User by RequestedBy (UserId)
                var user = await _context.Users.FindAsync(datasetRequest.RequestedBy);
                if (user == null)
                {
                    ModelState.AddModelError("RequestedBy", "The specified user does not exist.");
                    ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "UserId", "Username");
                    return View(datasetRequest);
                }

                // Link the User object to the DatasetRequest via foreign key
                datasetRequest.RequestedByUser = user;
                datasetRequest.Status = "Pending";
                datasetRequest.RequestedAt = System.DateTime.UtcNow;

                _context.Add(datasetRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                // Inspect errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(datasetRequest);
            }
            // Reload the users if the model is invalid
            ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "UserId", "Username");
            return View(datasetRequest);
        }


    }
}