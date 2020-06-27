using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aera_core.Domain;
using aera_core.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {

        private readonly ILogger<ClientesController> _logger;
        private readonly ClienteRepositório _repositório;

        public ClientesController(ILogger<ClientesController> logger, ClienteRepositório repositório)
        {
            _logger = logger;
            _repositório = repositório;
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return _repositório.ObterClientes();
        }
    }
}
