using ClientDirectory.Core.Base;
using ClientDirectory.DataAccess.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClientDirectory.Controllers.Base
{
    public class BaseController<T> : ControllerBase
        where T : class, IAuditableEntity
    {
        private readonly IBaseRepository<T> _baseRepository;
        public BaseController(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(_baseRepository.GetAll());
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var response = _baseRepository.Get(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public virtual IActionResult Post(T entity)
        {
            var response = _baseRepository.Create(entity);

            return Ok(response);
        }

        [HttpPut]
        public virtual IActionResult Put(T entity)
        {
            var response = _baseRepository.Update(entity);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual IActionResult Delete(int id)
        {
            var entity = _baseRepository.Get(id);

            var response = _baseRepository.Delete(entity);

            return Ok(response);
        }
    }
}
