using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageRepository<ChatMessage> repository;


        public ChatController(IChatMessageRepository<ChatMessage> repository)
        {
            this.repository = repository;
        }

        // Returns the full chat history from the database
        [HttpGet("history")]
        public async Task<IActionResult> GetChatHistory()
        {
            try
            {
                var messages = await repository.GetAllMessagesAsync();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving chat history: {ex.Message}");
            }
        }

        // Deletes all chat history from the database
        [HttpDelete("delete/history")]
        public async Task<IActionResult> ClearHistory()
        {
            try
            {
                await repository.ClearHistory();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while clearing chat history: {ex.Message}");
            }
        }
    }
}