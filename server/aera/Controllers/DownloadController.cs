using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Models;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PugPdf.Core;
using RazorEngineCore;

namespace aera_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly TurmasServiço _turmasServiço;
        private readonly ClientesServiço _clientesServiço;

        public DownloadController(TurmasServiço turmasServiço, ClientesServiço clientesServiço)
        {
            _turmasServiço = turmasServiço;
            _clientesServiço = clientesServiço;
        }

        [HttpGet("atestado/{alunoId}")]
        public async Task<IActionResult> geraAtestado(int alunoId) 
        {
            var renderer = new HtmlToPdf();
            renderer.PrintOptions.Title = "Lista de Presença";
            
            var razorEngine = new RazorEngine();
            var template = razorEngine.Compile(await System.IO.File.ReadAllTextAsync("Templates/Atestado.cshtml"));

            var aluno = _clientesServiço.Obter(alunoId);
            var turmas = _turmasServiço.ObterTurmasDosAlunos(new List<int> {aluno.Id});
            string result = template.Run( new
            {
                Aluno = aluno,
                Turmas = turmas
            });

            var pdf = await renderer.RenderHtmlAsPdfAsync(result);
            return new FileStreamResult(new MemoryStream(pdf.BinaryData),"application/pdf");
        }
        
        [HttpGet("listaDePresenca/{id}")]
        public async Task<IActionResult> geraLista(int id) 
        {
            var renderer = new HtmlToPdf();
            renderer.PrintOptions.Title = "Lista de Presença";
            
            var razorEngine = new RazorEngine();
            var template = razorEngine.Compile(await System.IO.File.ReadAllTextAsync("Templates/ListaDePresenca.cshtml"));

            var turma = _turmasServiço.Obter(id);
            turma.Alunos = turma.Alunos.OrderBy(a => a.name).ToList();
            string result = template.Run(turma);

            var pdf = await renderer.RenderHtmlAsPdfAsync(result);
            return new FileStreamResult(new MemoryStream(pdf.BinaryData),"application/pdf");
        }
        
    }
}
