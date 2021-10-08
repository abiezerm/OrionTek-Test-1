using ClientDirectory.Controllers.Base;
using ClientDirectory.Core;
using ClientDirectory.DataAccess.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : BaseController<Client>
    {
        private readonly IBaseRepository<Client> _baseRepository;

        public ClientController(IBaseRepository<Client> baseRepository) : base(baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        [HttpGet]
        public override IActionResult Get()
        {
            return Ok(_baseRepository.GetAll(clients => clients.Include(client => client.Addresses), client => !client.IsDeleted));
        }

        [HttpGet("{id}")]
        public override IActionResult Get(int id)
        {
            return Ok(_baseRepository.Get(clients => clients.Include(client => client.Addresses), client => client.Id == id && !client.IsDeleted));
        }
    }
}
