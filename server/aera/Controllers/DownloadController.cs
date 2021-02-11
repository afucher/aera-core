using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aera_core.Domain;
using Microsoft.AspNetCore.Mvc;
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
            var matriculas = _turmasServiço.ObterMatriculas(aluno.Id)
                .Where(m => m.Pagamentos.All(p => p.Paid ?? false));
            string result = template.Run( new
            {
                Aluno = aluno,
                Matriculas = matriculas
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
