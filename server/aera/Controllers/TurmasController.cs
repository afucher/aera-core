using System;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly TurmasServiço _turmasServiço;
        private readonly CursosServiço _cursoServiço;
        private readonly ProfessoresServiço _professoresServiço;
        private readonly ClientesServiço _clientesServiço;
        private readonly PagamentosServiço _pagamentosServiço;


        public TurmasController(TurmasServiço turmasServiço, CursosServiço cursoServiço, ProfessoresServiço professoresServiço, ClientesServiço clientesServiço, PagamentosServiço pagamentosServiço)
        {
            _turmasServiço = turmasServiço;
            _cursoServiço = cursoServiço;
            _professoresServiço = professoresServiço;
            _clientesServiço = clientesServiço;
            _pagamentosServiço = pagamentosServiço;
        }

        [HttpGet("{id}")]
        public TurmaDTO Get(int id)
        {
            var today = DateTime.Today;
            var primeiroDiaDoMesAtual = new DateTime(today.Year, today.Month, 1);       
            var turma = _turmasServiço.Obter(id);
            var turmasExtras = _turmasServiço.
                ObterTurmasDosAlunos(turma.Alunos.Select(a => a.id).ToList())
                .Where(t => t.id != id && t.end_date >= primeiroDiaDoMesAtual )
                .ToList();
            var turmaDto = TurmaDTO.De(turma);
            if (turmaDto.Alunos == null) return turmaDto;

            foreach (var aluno in turmaDto.Alunos)
            {
                aluno.turmas = 
                    turmasExtras
                        .Where(t => t.Alunos.Select(a => a.id)
                            .Contains(aluno.id))
                        .Select(TurmaDTO.De)
                        .ToList();
            }

            return turmaDto;
        }
        
        [HttpPost]
        public ActionResult<TurmaDTO> Post([FromBody] TurmaDTO turmaDTO)
        {
            var curso = _cursoServiço.Obter(turmaDTO.CursoId);
            var professor = _professoresServiço.Obter(turmaDTO.ProfessorId);
            if (professor == null) return BadRequest("Professor não existe");
            var turma = turmaDTO.ParaModelo();
            turma.Curso = curso;
            var turmaCriada = _turmasServiço.Criar(turma);
            
            if (turmaCriada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaCriada);
        }
        
        [HttpPut("{id}")]
        public ActionResult<TurmaDTO> Put(int id, [FromBody] TurmaDTO turma)
        {
            if (!id.Equals(turma.Id)) return BadRequest("Id não é válido");
            var professor = _professoresServiço.Obter(turma.ProfessorId);
            if (professor == null) return BadRequest("Professor não existe");
            var turmaAtualizada = _turmasServiço.Atualizar(turma.ParaModelo());
            
            if (turmaAtualizada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaAtualizada);
        }

        [HttpGet]
        public POUIListResponse<TurmaDTO> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
            };
            var turmas = _turmasServiço.ObterTurmas(opções);
            var turmasDto = turmas.Select(TurmaDTO.De).ToArray();

            return new POUIListResponse<TurmaDTO>(turmasDto, turmas.TemMaisItens);
        }

        [HttpGet("pagamentos")]
        public ActionResult<object> Pagamentos(DateTime De, DateTime Ate)
        {
            var turmas = _pagamentosServiço.ObterTurmasComPagamentoAberto(De, Ate);
            return turmas.Select(t => new
            {
                t.id,
                DataInicial = t.start_date.ToString("yyyy-MM-dd"),
                DataFinal = t.end_date.ToString("yyyy-MM-dd"),
                HorárioInicial = t.start_hour.ToString(@"hh\:mm"),
                HorárioFinal = t.end_hour.ToString(@"hh\:mm"),
                NomeCurso = t.Curso.name,
                DiaDaSemana = t.start_date.DayOfWeek,
                Matriculas = t.TurmaAlunos
                    .Select(m => new
                    {
                        m.id,
                        pagamentos = m.Pagamentos.Select(p => new
                        {
                            Pago = p.Paid,
                            DataDeVencimento = p.DueDate,
                            Parcela = p.Installment,
                            TotalDeParcelas = p.NumberInstallments,
                            Valor = p.Value
                        }), 
                        aluno = new
                        {
                            m.Cliente.id,
                            Nome = m.Cliente.name
                        }
                    })
            }).ToList();
        }
        
        [HttpPost("{id}/matricular")]
        public ActionResult<TurmaDTO> Matricular(int id, [FromBody] int alunoId)
        {
            var aluno = _clientesServiço.Obter(alunoId);
            if (aluno == null) return BadRequest("Aluno não existe");

            var turma = _turmasServiço.Obter(id);
            if (turma == null) return BadRequest("Turma não existe");
            var turmaAtualizada = _turmasServiço.MatricularAluno(turma, aluno);
            
            if (turmaAtualizada == null) return BadRequest("Erro ao salvar");
            
            return TurmaDTO.De(turmaAtualizada);
        }

        [HttpPost("{id}/pagamentos")]
        public ActionResult<TurmaDTO> GerarPagamentos(int id, [FromQuery]int parcelas,[FromQuery] decimal valor,[FromQuery] DateTime dataVencimento)
        {
            var turma = _turmasServiço.Obter(id);
            if (turma == null) return BadRequest("Turma não existe");
            
            _pagamentosServiço.GerarPagamentos(turma, parcelas, valor, dataVencimento);
            
            return TurmaDTO.De(_turmasServiço.Obter(id));
        }
    }
}
