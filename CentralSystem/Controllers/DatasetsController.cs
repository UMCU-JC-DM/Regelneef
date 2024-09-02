using CentralSystem.Data;
using CentralSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DatasetsController : ControllerBase
{
    private readonly CentralSystemContext _context;

    public DatasetsController(CentralSystemContext context)
    {
        _context = context;
    }

    // GET: api/Datasets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dataset>>> GetDatasets()
    {
        return await _context.Datasets.ToListAsync();
    }

    // GET: api/Datasets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Dataset>> GetDataset(int id)
    {
        var dataset = await _context.Datasets.FindAsync(id);

        if (dataset == null)
        {
            return NotFound();
        }

        return dataset;
    }

    // POST: api/Datasets
    [HttpPost]
    public async Task<ActionResult<Dataset>> PostDataset(Dataset dataset)
    {
        _context.Datasets.Add(dataset);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDataset", new { id = dataset.DatasetId }, dataset);
    }
}
