using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Core.Services;
using VetApp.Core.Models;
using AutoMapper;
using VetApp.Resources;
using Microsoft.AspNetCore.Authorization;

namespace VetApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;
        private readonly IMapper mapper;
        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceService = serviceService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ServiceResource>>> GetAll()
        {
            string iden = User.Identity.Name;
            var services = await serviceService.GetAll(iden);
            var serviceResource = mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return Ok(serviceResource);
        }

        [HttpGet("search/{param}")]
        public async Task<ActionResult<IEnumerable<ServiceResource>>> Search(string param)
        {
            string iden = User.Identity.Name;
            var services = await serviceService.Search(iden, param);
            var serviceResource = mapper.Map<IEnumerable<Service>, IEnumerable<ServiceResource>>(services);
            return Ok(serviceResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResource>> GetServiceById(int id)
        {
            string iden = User.Identity.Name;
            var service = await serviceService.GetServiceById(id, iden);
            var serviceResource = mapper.Map<Service, ServiceResource>(service);
            return Ok(serviceResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ServiceResource>> CreateService([FromBody] ServiceResource serviceResource)
        {
            var serviceToCreate = mapper.Map<ServiceResource, Service>(serviceResource);
            string iden = User.Identity.Name;
            serviceToCreate.VetName = iden;
            var newService = await serviceService.CreateService(serviceToCreate);
            var service = await serviceService.GetServiceById(newService.Id, iden);
            var serviceResResource = mapper.Map<Service, ServiceResource>(service);
            return Ok(serviceResResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResource>> DeleteService(int id)
        {
            string iden = User.Identity.Name;
            var service = await serviceService.GetServiceById(id, iden);
            if (service != null) await serviceService.DeleteService(service);
            return Ok(service);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResource>> UpdateService(int id, [FromBody] ServiceResource serviceResource)
        {
            string iden = User.Identity.Name;
            serviceResource.VetName = iden;
            var service = mapper.Map<ServiceResource, Service>(serviceResource);
            await serviceService.UpdateService(id, service);

            var updatedService = await serviceService.GetServiceById(id, iden);
            var updatedServiceResource = mapper.Map<Service, ServiceResource>(updatedService);
            return Ok(updatedServiceResource);
        }
    }
}

