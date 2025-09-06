using Backend.API.Data;
using Backend.API.Models.Domain;
using Backend.API.Models.DTO;
using Backend.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        
        private readonly ICustomerRepository operations;
        public CustomerController(ICustomerRepository operations,ApplicationDBContext _dbContext)
        {
            this.dbContext = _dbContext;
            this.operations = operations;
        }
        [HttpPost]
        public async Task<IActionResult> addCustomerMethod(AddMineRequestDTO request)
        {
            var mineDetails = new MineDetails
            {
                mineName = request.MineName,
                ContactName = request.ContactName,
                ContactNumber = request.ContactNumber
            };
            await operations.addMineAsync(mineDetails);

            var response = new MineDetailsDTO
            {
                Id=(Guid)mineDetails.Id,
                MineName=mineDetails.mineName,
                ContactName=mineDetails.ContactName,
                ContactNumber=mineDetails.ContactNumber
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> getCustomerMethod()
        {
            var data = await operations.listMineAsync();
            return Ok(data);
        }
        public class mineMergerClass
        {
            public AddMineRequestDTO targetMine { get; set; }
            public AddMineRequestDTO[] minesToMerge { get; set; }
        }
        [HttpPost("merge-mines")]

        public async Task<IActionResult> postMinesMethod(mineMergerClass request)
        {
            var targetMine = await dbContext.MineDetails.FindAsync(request.targetMine.Id);
            List<Guid> sourceMines = new List<Guid>();

            if (targetMine == null)
            {
                return NotFound("The target Mine Not Found");
            }

            foreach (var mine in request.minesToMerge)
            {
                var sourceMine = await dbContext.MineDetails.FindAsync(mine.Id);
                if (sourceMine == null)
                {
                    return NotFound("The Source Mine Not Found");
                }

                sourceMines.Add((Guid)sourceMine.Id);

                // ✅ Await ForEachAsync to avoid concurrent queries
                await dbContext.OrderHistory
                    .Where(oh => oh.MineId == sourceMine.Id)
                    .ForEachAsync(oh =>
                    {
                        oh.MineId = (Guid)targetMine.Id;
                    });

                var mineItemRates = await dbContext.MineItemRate
                    .Where(mir => mir.MineDetailsId == sourceMine.Id)
                    .ToListAsync();

                dbContext.MineItemRate.RemoveRange(mineItemRates);
                await dbContext.SaveChangesAsync(); // ✅ finish delete before next step

                dbContext.MineDetails.Remove(sourceMine);
                await dbContext.SaveChangesAsync(); // ✅ finish delete before looping
            }

            var returnObj = new
            {
                targetMine = request.targetMine,
                minesToMerge = request.minesToMerge
            };

            return Ok(returnObj);
        }
    }

}
