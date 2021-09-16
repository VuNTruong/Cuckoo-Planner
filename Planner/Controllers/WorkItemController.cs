using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Planner.Data;
using Planner.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkItemController : Controller
    {
        // GET: /<controller>/
        [HttpGet("View")]
        public IActionResult Index()
        {
            ViewData["Header"] = "Hello there!";
            return View();
        }

        // The function to get all work items in the database
        [HttpGet]
        public async Task<JsonResult> getWorkItems()
        {
            using (var db = new DatabaseContext())
            {
                // Start querying the database
                var posts = await db.Posts
                    .ToListAsync();

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Add data to the response data
                Response.StatusCode = 200;
                responseData.Add("status", "Done");
                responseData.Add("data", posts);

                // Return response to the client
                return new JsonResult(responseData);
            }
        }

        // The function to create new work item in the database
        [HttpPost]
        public async Task<JsonResult> createWorkItem()
        {
            using (var db = new DatabaseContext())
            {
                // Use this to read request body
                var inputData = await new StreamReader(Request.Body).ReadToEndAsync();

                // Convert JSON object into accessible object
                var requestBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(inputData);

                // Create the new object which is going to be inserted into the database
                WorkItem newWorkItem = new WorkItem(requestBody["title"], requestBody["content"], requestBody["dateCreated"]);

                // Start querying the database
                await db.Posts
                    .AddAsync(newWorkItem);

                // Save changes
                await db.SaveChangesAsync();

                // Prepare response data for the client
                var responseData = new Dictionary<string, object>();

                // Add data to the response data
                Response.StatusCode = 201;
                responseData.Add("status", "Done");
                responseData.Add("data", newWorkItem);

                // Return response to the client
                return new JsonResult(responseData);
            }
        }
    }
}
