using System;
using System.Collections.Generic;
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
        public POUIListResponse<PagamentoDTO> Get([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string search)
        {
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
            };
            var pagamentos = _pagamentosServiço.ObterPagamentos(opções);
            var pagamentosDTO = pagamentos.Select(PagamentoDTO.De).ToArray();

            return new POUIListResponse<PagamentoDTO>(pagamentosDTO, pagamentos.TemMaisItens);
        }
        
        [HttpGet("matricula/{matriculaId}")]
        public List<PagamentoDTO> Get(int matriculaId)
        {
            var pagamentos = _pagamentosServiço.ObterPorMatricula(matriculaId);
            return pagamentos.Select(PagamentoDTO.De).ToList();
        }
        
        [HttpGet("turma/{turmaId}/aluno/{alunoId}")]
        public List<PagamentoDTO> Get(int turmaId, int alunoId)
        {
            var pagamentos = _pagamentosServiço.ObterDoAlunoDaTurma(turmaId, alunoId);
            return pagamentos.Select(PagamentoDTO.De).ToList();
        }

        [HttpPost]
        public PagamentoDTO Pagar([FromBody] PagamentoDTO pagamento)
        {
             var pagamentoPago = _pagamentosServiço.Pagar(pagamento.IdMatricula, pagamento.Parcela);
             return PagamentoDTO.De(pagamentoPago);

        }
    }
}
