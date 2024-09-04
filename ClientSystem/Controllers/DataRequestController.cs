using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientSystem.Data;
using ClientSystem.Models;

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

        // GET: DatasetRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DatasetRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestedBy,DatasetType")] DatasetRequest datasetRequest)
        {
            if (ModelState.IsValid)
            {
                datasetRequest.Status = "Pending";
                datasetRequest.RequestedAt = DateTime.UtcNow;
                _context.Add(datasetRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(datasetRequest);
        }
    }
}