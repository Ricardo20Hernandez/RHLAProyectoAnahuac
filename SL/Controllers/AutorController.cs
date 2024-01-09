using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        [EnableCors("API")]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Autor.GetAll();
            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // POST api/<EmpleadoController>
        [EnableCors("API")]
        [HttpPost("add")]
        public IActionResult Add([FromBody] ML.Autor autor)
        {
            ML.Result result = BL.Autor.Add(autor);
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
        public IActionResult Update([FromBody] ML.Autor autor)
        {
            ML.Result result = BL.Autor.Update(autor);
            if (result.Correct) { 
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [EnableCors("API")]
        [HttpGet("{IdAutor}")]
        public IActionResult GetById(int IdAutor)
        {
            ML.Result result = BL.Autor.GetById(IdAutor);

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
        [HttpPost("convert")]
        public IActionResult Convert(IFormFile fuImagen)
        {
            ML.Autor autor = new ML.Autor();

            MemoryStream target = new MemoryStream();
            fuImagen.OpenReadStream().CopyTo(target);
            byte[] data = target.ToArray();

            autor.Foto = data;

            if (autor.Foto != null)
            {
                return Ok(autor.Foto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
