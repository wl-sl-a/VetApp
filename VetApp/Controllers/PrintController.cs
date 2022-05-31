using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Services;
using VetApp.Core.Models;
using AutoMapper;
using VetApp.Resources;
using VetApp.Authentication;
using Microsoft.AspNetCore.Authorization;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Charting;
using PdfSharp.Internal;
using System.Diagnostics;

namespace VetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : Controller
    {
        private readonly IVisitingService visitingService;
        private readonly IMapper mapper;

        public PrintController(IVisitingService visitingService, IMapper mapper)
        {
            this.mapper = mapper;
            this.visitingService = visitingService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Visiting>> PrintVisitingById(int id)
        {
            string iden = User.Identity.Name;
            var visiting = await visitingService.GetVisitingById(id, iden);
            visitingService.print(iden+".pdf", visiting);

            return Ok(visiting);
        }
    }
}
