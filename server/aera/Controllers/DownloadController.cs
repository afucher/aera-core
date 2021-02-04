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

        public DownloadController(TurmasServiço turmasServiço)
        {
            _turmasServiço = turmasServiço;
        }

        [HttpGet("listaDePresenca/{id}")]
        public async Task<IActionResult> geraPDF(int id) 
        {
            var renderer = new HtmlToPdf();
            renderer.PrintOptions.Title = "Lista de Presença";
            
            var razorEngine = new RazorEngine();
            var template = razorEngine.Compile(await System.IO.File.ReadAllTextAsync("Templates/ListaDePresenca.cshtml"));

            string result = template.Run(_turmasServiço.Obter(id));

            var pdf = await renderer.RenderHtmlAsPdfAsync(result);
            return new FileStreamResult(new MemoryStream(pdf.BinaryData),"application/pdf");
        }
        
    }
}
