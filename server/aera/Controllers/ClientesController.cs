using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {

        private readonly ILogger<ClientesController> _logger;
        private readonly ClientesServiço _clientesServiço;

        public ClientesController(ILogger<ClientesController> logger, ClientesServiço clientesServiço)
        {
            _logger = logger;
            _clientesServiço = clientesServiço;
        }

        [HttpGet("{id}")]
        public ClienteDTO Get(int id)
        {
            var cliente = _clientesServiço.Obter(id);
            return new ClienteDTO
            {
                id = cliente.Id,
                nome = cliente.Nome,
                address1 =  cliente.address1,
                address2 =  cliente.address2,
                address3 =  cliente.address3,
                celular = cliente.Celular,
                cep = cliente.CEP,
                cidade = cliente.Cidade,
                cpf = cliente.CPF,
                data_nascimento = cliente.DataNascimento?.ToString("yyyy-MM-dd"),
                profissao = cliente.Profissão,
                professor = cliente.ÉProfessor,
                email = cliente.Email,
                estado = cliente.Estado,
                hora_nascimento = cliente.HorárioNascimento?.ToString(@"hh\:mm"),
                local_nascimento = cliente.LocalNascimento,
                nivel_educacao = cliente.NívelEducação,
                observacao = cliente.Observação,
                telefone = cliente.Telefone,
                telefone_comercial = cliente.TelefoneComercial,
                turmas = cliente.Turmas.Select(t => new TurmaDTO
                {
                    Curso = t.Curso.name,
                    DataInicial = t.start_date.ToString("yyyy-MM-dd"),
                    DataFinal = t.end_date.ToString("yyyy-MM-dd")
                }).ToList()
            };
        }

        [HttpGet]
        public POUIListResponse<ClienteDTO> Get([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string search, [FromQuery] string filter)
        {
            var filtro = search == null ? filter : search;
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
                Filtros = new Dictionary<string, string>
                {
                    { "search", filtro }
                }
            };
            var clientes = _clientesServiço.ObterClientes(opções);
            var clientesDTO = clientes.Select(cliente => new ClienteDTO
            {
                id = cliente.Id,
                nome = cliente.Nome,
                address1 =  cliente.address1,
                address2 =  cliente.address2,
                address3 =  cliente.address3,
                celular = cliente.Celular,
                cep = cliente.CEP,
                cidade = cliente.Cidade,
                cpf = cliente.CPF,
                data_nascimento = cliente.DataNascimento?.ToString("yyyy-MM-dd"),
                profissao = cliente.Profissão,
                professor = cliente.ÉProfessor,
                email = cliente.Email,
                estado = cliente.Estado,
                hora_nascimento = cliente.HorárioNascimento?.ToString(@"hh\:mm"),
                local_nascimento = cliente.LocalNascimento,
                nivel_educacao = cliente.NívelEducação,
                observacao = cliente.Observação,
                telefone = cliente.Telefone,
                telefone_comercial = cliente.TelefoneComercial

            }).ToArray();

            return new POUIListResponse<ClienteDTO>(clientesDTO, clientes.TemMaisItens);
        }

        [HttpPost]
        public ClienteDTO Post([FromBody] ClienteDTO cliente)
        {
            var clienteCriado = _clientesServiço.Criar(ClienteDB.DeCliente(cliente.ParaModelo()));
            return ClienteDTO.DoModelo(clienteCriado.ParaCliente());
        }
        
        [HttpPut("{id}")]
        public ActionResult<ClienteDTO> Put(int id, [FromBody] ClienteDTO cliente)
        {
            if (!id.Equals(cliente.id)) return BadRequest("Id não é válido");
            _clientesServiço.Atualizar(ClienteDB.DeCliente(cliente.ParaModelo()));
            return cliente;
        }
    }
}
