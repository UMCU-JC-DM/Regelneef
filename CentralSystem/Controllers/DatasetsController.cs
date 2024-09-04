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
public async Task<ActionResult<Dataset>> PostDataset([FromBody] Dataset dataset)
{
    // Check if the user exists in the database
    var existingUser = await _context.Users.FindAsync(dataset.CreatedBy);

    if (existingUser != null)
    {
        // If the user exists, attach it as unchanged to avoid creating a new one
        _context.Entry(existingUser).State = EntityState.Unchanged;
        dataset.CreatedByUser = existingUser;
    }
    else
    {
        // If the user doesn't exist, create a new one (assuming the dataset has all necessary user details)
        // Adjust this part based on how you would handle user creation details
        var newUser = new User
        {
            UserId = dataset.CreatedBy, // Ensure the UserId is passed in the dataset or generated
            Username = dataset.CreatedByUser.Username,      // Replace with actual values or input from the request
            Role = dataset.CreatedByUser.Role,        // Set the default role or input from the request
            LastLogin = DateTime.Now
        };

        _context.Users.Add(newUser);
        dataset.CreatedByUser = newUser;
    }

    _context.Datasets.Add(dataset);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetDataset", new { id = dataset.DatasetId }, dataset);
}
}
