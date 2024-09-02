using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuditLogsController : ControllerBase
{
    private readonly CentralSystemContext _context;

    public AuditLogsController(CentralSystemContext context)
    {
        _context = context;
    }

    // GET: api/AuditLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs()
    {
        return await _context.AuditLogs.ToListAsync();
    }

    // GET: api/AuditLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuditLog>> GetAuditLog(int id)
    {
        var auditLog = await _context.AuditLogs.FindAsync(id);

        if (auditLog == null)
        {
            return NotFound();
        }

        return auditLog;
    }

    // POST: api/AuditLogs
    [HttpPost]
    public async Task<ActionResult<AuditLog>> PostAuditLog(AuditLog auditLog)
    {
        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAuditLog", new { id = auditLog.LogId }, auditLog);
    }
}
