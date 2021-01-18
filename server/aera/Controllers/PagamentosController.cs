using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Models;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly PagamentosServiço _pagamentosServiço;

        public PagamentosController(PagamentosServiço pagamentosServiço)
        {
            _pagamentosServiço = pagamentosServiço;
        }

        [HttpGet]
        public POUIListResponse<PagamentoDB> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
            };
            var pagamentos = _pagamentosServiço.ObterPagamentos(opções);
            // var cursosDTO = pagamentos.Select(curso => CursoDTO.De(curso)).ToArray();

            return new POUIListResponse<PagamentoDB>(pagamentos, pagamentos.TemMaisItens);
        }
    }
}
