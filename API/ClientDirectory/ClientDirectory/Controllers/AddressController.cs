using ClientDirectory.Controllers.Base;
using ClientDirectory.Core;
using ClientDirectory.DataAccess.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClientDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IBaseRepository<Address> baseRepository) : base(baseRepository)
        {
        }
    }
}
