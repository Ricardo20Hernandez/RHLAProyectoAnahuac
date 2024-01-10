using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EditorialController : Controller
    {
        [EnableCors("API")]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Editorial.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [EnableCors("API")]
        [HttpGet("paisgetall")]
        public IActionResult PaisGetAll()
        {
            ML.Result result = BL.Pais.GetAll();
            
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpGet("estadogetbyidpais")]
        public IActionResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpGet("municipiogetbyidestado")]
        public IActionResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            } 
        }

        [EnableCors("API")]
        [HttpGet("coloniagetbyidmunicipio")]
        public IActionResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpPost("add")]
        public IActionResult Add([FromBody] ML.Editorial editorial) 
        {
        
            ML.Result result = BL.Editorial.Add(editorial);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [EnableCors("API")]
        [HttpPut("update")]
        public IActionResult Update([FromBody] ML.Editorial editorial)
        {
            ML.Result result = BL.Editorial.Update(editorial);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [EnableCors("API")]
        [HttpGet("getbyid")]
        public IActionResult GetById(int IdEditorial)
        {
            ML.Result resultGetById = BL.Editorial.GetById(IdEditorial);

            if(resultGetById.Correct) 
            {
                return Ok(resultGetById);
            }
            else
            {
                return BadRequest(resultGetById);
            }
        }
    }
}
