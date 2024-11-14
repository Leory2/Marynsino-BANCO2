using Marynsino2513.Models;
using Marynsino2513.ORM;
using Marynsino2513.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Marynsino2513.Controllers
{
    public class ServicoController : Controller
    {
        private readonly BdMarynsinoContext _context;

        public ServicoController(BdMarynsinoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Recuperando todos os serviços da tabela TbServicos
            var servicos = _context.TbServicos
                .Select(s => new ServicoVM
                {
                    Id = s.Id,
                    TipoServico = s.TipoServico,
                    Valor = s.Valor
                })
                .ToList();  // Convertendo para uma lista de ServicoVM

            return View(servicos);  // Passando a lista de ServicoVM para a view
        }
    
    
    }
}