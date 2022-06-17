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
using System.Security.Claims;
using System;

namespace VetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : Controller
    {
        private readonly IVisitingService visitingService;
        private readonly IDirectionService directionService;
        private readonly IMapper mapper;

        public PrintController(IVisitingService visitingService, IMapper mapper, IDirectionService directionService)
        {
            this.mapper = mapper;
            this.visitingService = visitingService;
            this.directionService = directionService;
        }

        [HttpGet("visiting/{id}")]
        public async Task<ActionResult<Visiting>> PrintVisitingById(int id)
        {
            string iden = User.Identity.Name;
            var visiting = await visitingService.GetVisitingById(id, iden);
            visitingService.print(DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss") + ".pdf", visiting, iden);

            return Ok();
        }

        [HttpGet("direction/{id}")]
        public async Task<ActionResult<Visiting>> PrintDirectionById(int id)
        {
            string iden = User.Identity.Name;
            var direction = await directionService.GetDirectionById(id, iden);
            directionService.print(DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss") + ".pdf", direction, iden);

            return Ok();
        }
    }
}
