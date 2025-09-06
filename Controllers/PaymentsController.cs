using Backend.API.Data;
using Backend.API.Models.Domain;
using Backend.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        ApplicationDBContext dbContext;
        public PaymentsController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> addPayment(PaymentHistoryRequestDTO request)
        {
            if (request == null) return BadRequest("Empty Parameters Supplied");
            var paymentDetails = new PaymentHistory()
            {
                Id=Guid.NewGuid(),
                MineId = request.MineId,
                Mine=await dbContext.MineDetails.FindAsync(request.MineId),
                PaymentDate = request.PaymentDate,
                Amount=request.Amount,
                PaymentMode=request.PaymentMode
            };
            await dbContext.PaymentHistory.AddAsync(paymentDetails);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> getPaymentHistory()
        {
            var paymentHistory = await dbContext.PaymentHistory.Select(ph => new PaymentHistoryResponseDTO{ 
                Amount=ph.Amount,
                MineId=ph.MineId,
                MineName=ph.Mine.mineName,
                PaymentDate=ph.PaymentDate,
                PaymentMode=ph.PaymentMode
            }).ToListAsync();
            return Ok(paymentHistory);
        }
    }
}
