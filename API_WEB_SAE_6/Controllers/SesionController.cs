using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using API_WEB_SAE_6.Models;
using API_WEB_SAE_6.Logs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace API_WEB_SAE_6.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SesionesController : Controller
    {
        private readonly string secretKey;
        /// <summary>
        /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
        /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
        /// </summary>
        private readonly Logger _logger = new();
        /// <summary>
        /// Recupera el contexto desde el archivo API_WEB_SAEContext <br/>
        /// </summary>
        /// <param name="context">
        /// Es el acceso a la base de datos con las credenciales de owner <br/>
        /// </param>
        public SesionesController( IConfiguration config)
        {
            secretKey = config.GetSection("Settings").GetSection("secretkey").ToString() ?? "ERROR";
        }
    }
}
