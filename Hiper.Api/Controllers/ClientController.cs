using Hiper.Domain.DTO.Client;
using Hiper.Services;
using Hiper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hiper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        // GET: Client
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] ClientFilterDTO filters)
        {
            try
            {
                var ret = await _clientServices.GetAll(filters);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                return Ok(await _clientServices.GetById(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Route("document")]
        public async Task<IActionResult> GetbyDocument(string document)
        {
            try
            {
                return Ok(await _clientServices.GetByDocument(document));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post([FromBody] ClientDTO clientDTO)
        {
            try
            {
                var ret = await _clientServices.Add(clientDTO);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("excel")]
        public async Task<IActionResult> GenerateExcel([FromBody] List<ClientDTO> clientsDTO)
        {
            try
            {
                return Ok(await _clientServices.GenerateExcel(clientsDTO));
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("consumeClients")]
        public async Task<IActionResult> ConsumeClients()
        {
            try
            {
                return Ok(await _clientServices.ConsumeClients());
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        [HttpPost]
        [Route("importClients")]
        public async Task<IActionResult> ImportarClientes([FromBody] string arquivo)
        {
            try
            {
                return Ok(await _clientServices.ImportClients(arquivo));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("alter")]
        public async Task<IActionResult> Put([FromBody] ClientDTO clientDTO)
        {
            try
            {
                return Ok(await _clientServices.Alter(clientDTO));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> Delete([FromBody] ClientDTO clientDTO)
        {
            try
            {
                return Ok(await _clientServices.Remove(clientDTO));
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}
