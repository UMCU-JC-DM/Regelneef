using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientSystem.Data;
using ClientSystem.Models;

namespace ClientSystem.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly ClientSystemContext _context;

        public AuditLogController(ClientSystemContext context)
        {
            _context = context;
        }

        // GET: AuditLogs
        public async Task<IActionResult> Index()
        {
            var auditLogs = await _context.AuditLogs.Include(a => a.User).ToListAsync();
            return View(auditLogs);
        }

        // POST: AuditLog/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserId,Action,Details")] AuditLog auditLog)
        {
            if (ModelState.IsValid)
            {
                auditLog.Timestamp = DateTime.UtcNow;
                _context.Add(auditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auditLog);
        }
    }
}