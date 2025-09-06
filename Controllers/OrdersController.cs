using Backend.API.Data;
using Backend.API.Models.Domain;
using Backend.API.Models.DTO;
using Backend.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;
        private readonly ITelegramBot bot;
        public OrdersController(ApplicationDBContext dBContext, ITelegramBot bot) {
            this.dBContext = dBContext;
            this.bot = bot;
        }

        [HttpPost]
        public async Task<IActionResult> post(OrderHistoryRequestDTO request)
        {
            var mine = await dBContext.MineDetails.Include(m => m.MineItems).FirstOrDefaultAsync(min => min.Id == request.mineDetails.Id);
            var originalOrder = new OrderHistoryRequestDTO();
            var message = new Bot();
            message.newOrder = false;
            if (mine == null)
            {
                mine = await dBContext.MineDetails.Include(m=>m.MineItems).FirstOrDefaultAsync(m => m.mineName == request.mineDetails.mineName);
            }
            var newMinesAdded=new List<MineDetails>();
            var newItemsAdded=new List<Item>();

            
            if (mine == null)
            {
                mine = new MineDetails
                {
                    Id = Guid.NewGuid(),
                    mineName = request.mineDetails.mineName,
                    ContactName = "Unknown",
                    ContactNumber = 0,
                    MineItems=new List<MineItemRate>()
                };
                newMinesAdded.Add(mine);

                await dBContext.MineDetails.AddAsync(mine);
                newMinesAdded.Add(mine);
            }

            foreach (var order in request.Orders)
            {
                Item item = await dBContext.Item.FindAsync(order.item.Id) ?? await dBContext.Item.FirstOrDefaultAsync(i => i.Name == order.item.Name);
                if (item == null)
                {
                    item = new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = order.item.Name,
                        PricePerUnit = 1
                    };
                    //if (order.itemPricePerUnit > 1)
                    //{
                    //    item.PricePerUnit = order.itemPricePerUnit;
                    //}
                    await dBContext.Item.AddAsync(item);
                    newItemsAdded.Add(item);
                }
                if (mine.MineItems.FirstOrDefault(mi => mi.ItemId == item.Id) == null)
                {
                    mine.MineItems.Add(new MineItemRate
                    {
                        Id = Guid.NewGuid(),
                        ItemId = item.Id,
                        Item = item,
                        PricePerUnit = order.itemPricePerUnit
                    });
                }
                if (mine.MineItems.FirstOrDefault(mi => mi.ItemId == item.Id).PricePerUnit < 2)
                {
                    mine.MineItems.FirstOrDefault(mi => mi.ItemId == item.Id).PricePerUnit=order.itemPricePerUnit;
                    //m.PricePerUnit=item.PricePerUnit
                }
            }

            OrderHistory orderHistory;
            if (request.id == null)
            {
                message.newOrder = true;
                orderHistory = new OrderHistory { Id = Guid.NewGuid() };
                dBContext.OrderHistory.AddAsync(orderHistory);
            }
            else
            {
                orderHistory = await dBContext.OrderHistory.FindAsync(request.id);
                if (orderHistory == null)
                {
                    orderHistory = new OrderHistory { Id = (Guid)request.id };
                    await dBContext.OrderHistory.AddAsync(orderHistory);
                }
            }
            orderHistory.Date = request.Date;
            orderHistory.MineId = (Guid)mine.Id;
            orderHistory.IsCompleted = request.isCompleted;
            orderHistory.Orders = new List<Order>();

            //await dBContext.OrderHistory.AddAsync(orderHistory);
            foreach (var o in request.Orders)
            {
                Item item = await dBContext.Item.FindAsync(o.item.Id) ?? await dBContext.Item.FirstOrDefaultAsync(i => i.Name == o.item.Name);

                if (item == null)
                {
                    item = new Item { Id = Guid.NewGuid(), Name = o.item.Name, PricePerUnit = 1 };
                    await dBContext.Item.AddAsync(item);
                    newItemsAdded.Add(item);
                }
                Order order = await dBContext.Order.FindAsync(o.Id);
                if (order == null)
                {
                    order = new Order
                    {
                        Id = Guid.NewGuid(),
                        ItemId = item.Id,
                        Item = item,
                        itemPricePerUnit=o.itemPricePerUnit,
                        Quantity = o.Quantity,
                        OrderValue = o.Quantity * o.itemPricePerUnit,
                        OrderHistoryId = orderHistory.Id

                    };
                    await dBContext.Order.AddAsync(order);
                }
                await dBContext.SaveChangesAsync();
            }
            //await dBContext.OrderHistory.AddAsync(orderHistory);
            await dBContext.SaveChangesAsync();
            var mineResponse = new { 
                added=newMinesAdded.Count>0,
                mines = newMinesAdded
            };
            var itemResponse = new
            {
                added = newItemsAdded.Count>0,
                items = newItemsAdded
            };
            message.order = new OrderHistoryRequestDTO
            {
                Date = orderHistory.Date,
                mineDetails=orderHistory.Mine,
                isCompleted = orderHistory.IsCompleted,
                Orders = orderHistory.Orders.Select(o => new OrderRequestDTO
                {
                    item = o.Item,
                    Quantity = o.Quantity
                }).ToList()
            };
            var messageText = new StringBuilder();

            messageText.AppendLine("📝 *Order Summary*");
            messageText.AppendLine($"📅 *Date:* {message.order.Date:yyyy-MM-dd}");
            messageText.AppendLine($"✅ *Status:* {(message.order.isCompleted ? "Completed" : "Pending")}");
            messageText.AppendLine();

            messageText.AppendLine("🏗️ *Mine Details:*");
            messageText.AppendLine($"- Name: {mine.mineName}");
            if (!string.IsNullOrWhiteSpace(mine.ContactName))
                messageText.AppendLine($"- Contact: {mine.ContactName}");
            messageText.AppendLine($"- Phone: {mine.ContactNumber}");
            messageText.AppendLine();

            messageText.AppendLine("📦 *Ordered Items:*");
            foreach (var o in message.order.Orders)
            {
                if(o.Quantity>0)messageText.AppendLine($"- {o.item.Name}: {o.Quantity}");
            }

            // Final message to pass to Telegram
            var formattedMessage = messageText.ToString();
            //bot.SendMessage(formattedMessage);
            return Ok(new { mineResponse, itemResponse, orderHistory.Id });
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var activeOrder = await dBContext.OrderHistory.Where(oh => !oh.IsCompleted).Select(oh => new OrderHistoryDTO
            {
                Id = oh.Id,
                Date = oh.Date,
                MineDetails = new MineDetails
                {
                    Id = oh.MineId,
                    mineName = oh.Mine.mineName,
                    ContactName = oh.Mine.ContactName,
                    ContactNumber = oh.Mine.ContactNumber
                },
                isCompleted = oh.IsCompleted,
                Orders = oh.Orders.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    Item = new Item
                    {
                        Id = o.Item.Id,
                        Name = o.Item.Name,
                        PricePerUnit = o.Item.PricePerUnit
                    },
                    itemPricePerUnit = o.itemPricePerUnit,
                    Quantity = o.Quantity,
                    OrderValue = o.OrderValue
                }).ToList()
            }).ToListAsync();

            var pastOrder = await dBContext.OrderHistory.Where(oh => oh.IsCompleted).Select(oh => new OrderHistoryDTO
            {
                Id = oh.Id,
                Date = oh.Date,
                MineDetails = new MineDetails
                {
                    Id = oh.MineId,
                    mineName = oh.Mine.mineName,
                    ContactName = oh.Mine.ContactName,
                    ContactNumber = oh.Mine.ContactNumber
                },
                isCompleted = oh.IsCompleted,
                Orders = oh.Orders.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    Item = new Item
                    {
                        Id = o.Item.Id,
                        Name = o.Item.Name,
                        PricePerUnit = o.Item.PricePerUnit
                    },
                    itemPricePerUnit = o.itemPricePerUnit,
                    Quantity = o.Quantity,
                    OrderValue = o.OrderValue
                }).ToList()
            }).ToListAsync();
            var orderHistory = new
            {
                ActiveOrders = activeOrder,
                PastOrders = pastOrder
            };
            return Ok(orderHistory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteOrder(Guid id)
        {
            var order = await dBContext.OrderHistory.FindAsync(id);
            if (order == null) return NotFound("Order Not Found");
            dBContext.OrderHistory.Remove(order);
            await dBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("for")]
        public async Task<IActionResult> getOrderByMineName([FromQuery] string? mineName, [FromQuery] DateOnly? date)
        {

            var mine = await dBContext.MineDetails.Include(m=>m.MineItems).FirstOrDefaultAsync(m => m.mineName == mineName);
            var orders = new List<OrderHistoryDTO>();
            if (mine == null)
            {
                //return NotFound("Mine Name specified could not be Found");
                if (date.HasValue)
                {
                    orders = await dBContext.OrderHistory.Where(oh => oh.Date == date).Select(oh=> new OrderHistoryDTO
                    {
                        Date=oh.Date,
                        Id=oh.Id,
                        isCompleted=oh.IsCompleted,
                        MineDetails=oh.Mine,
                        Orders=oh.Orders.Select(o=> new OrderDTO
                        {
                            Id = o.Id,
                            OrderValue = o.OrderValue,
                            Item = o.Item,
                            Quantity = o.Quantity
                        }).ToList()
                    }).ToListAsync();

                    return Ok(orders);
                }
                else
                {
                    return NotFound("Invalid or Empty Params");
                }
            }
            if (date.HasValue)
            {
                orders = await dBContext.OrderHistory.Where(oh => oh.MineId == mine.Id && oh.Date == date).Select(o => new OrderHistoryDTO()
                {
                    Id = o.Id,
                    Date = o.Date,
                    isCompleted = o.IsCompleted,
                    MineDetails=mine,
                    Orders = o.Orders.Select(o => new OrderDTO
                    {
                        Id = o.Id,
                        OrderValue = o.OrderValue,
                        Item = o.Item,
                        itemPricePerUnit=o.itemPricePerUnit,
                        Quantity = o.Quantity
                    }).ToList()
                }).ToListAsync();
            }
            else
            {
                orders = await dBContext.OrderHistory.Where(oh => oh.MineId == mine.Id).Select(o=>new OrderHistoryDTO
                {
                    Id = o.Id,
                    Date = o.Date,
                    isCompleted=o.IsCompleted,
                    MineDetails=mine,
                    Orders=o.Orders.Select(o=>new OrderDTO
                    {
                        Id = o.Id,
                        OrderValue = o.OrderValue,
                        Item = o.Item,
                        itemPricePerUnit = o.itemPricePerUnit,
                        Quantity = o.Quantity

                    }).ToList(),
                }).ToListAsync();
            }
                return Ok(orders);
        }
    }
}
