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
        private readonly ClientesServiço _clientesServiço;
        private readonly PagamentosServiço _pagamentosServiço;

        public ClientesController(ClientesServiço clientesServiço, PagamentosServiço pagamentosServiço)
        {
            _clientesServiço = clientesServiço;
            _pagamentosServiço = pagamentosServiço;
        }

        [HttpGet("{id}")]
        public ClienteDTO Get(int id)
        {
            var cliente = _clientesServiço.Obter(id);
            return ClienteDTO.DoModelo(cliente);
        }

        [HttpGet]
        public POUIListResponse<ClienteDTO> Get([FromQuery] int page, [FromQuery] int pageSize, 
            [FromQuery] string search, [FromQuery] string filter, [FromQuery] string nome, [FromQuery] string cpf)
        {
            var filtro = search == null ? filter : search;
            var opções = new OpçõesBusca
            {
                Página = page,
                LimitePágina = pageSize,
                Filtros = new Dictionary<string, string>
                {
                    { "search", filtro },
                    { "nome", nome },
                    {"cpf", cpf}
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
        public ActionResult<ClienteDTO> Post([FromBody] ClienteDTO cliente)
        {
            try
            {
                var clienteCriado = _clientesServiço.Criar(ClienteDB.DeCliente(cliente.ParaModelo()));
                return ClienteDTO.DoModelo(clienteCriado.ParaCliente());
            }
            catch (Exception e)
            {
                if (e.Message.Contains("CPF já cadastrado"))
                {
                    return BadRequest(e.Message);
                }

                throw;
            }
            
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
