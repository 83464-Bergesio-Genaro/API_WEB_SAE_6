using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PerfilesController : Controller
    {
        private static List<FuncionesXPerfiles> permisosRegistrados = new();
        /// <summary>
        /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
        /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
        /// </summary>
        private readonly Logger _logger = new();

        /// <summary>
        /// 
        /// </summary>
        public PerfilesController()
        {
        }

        [HttpGet("{id_perfil}/{id_funcion}")]
        [ActionName("TienePermiso")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<bool> TienePermiso(IConfiguration config, int id_perfil, int id_funcion)
        {
            //Si ya teiene en la cache no los vuelve a buscar
            if (permisosRegistrados != null && permisosRegistrados.Count > 0)
            {
                List<FuncionesXPerfiles> permisos = permisosRegistrados.Where(x => x.id_perfil == id_perfil
                && x.id_funcion == id_funcion).ToList();
                if (permisos.Count >= 1) return true;
                else return false;
            }
            else
            {
                //Busca en la vista de permisos y los deja cargados, son 300 registros aproximadamente
                try
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(config, "MODULO_USUARIOS_Vista_Permisos");

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return false;

                    permisosRegistrados = new();
                    foreach (DataRow dr in respuesta.Rows)
                    {
                        FuncionesXPerfiles fp = new(dr);
                        permisosRegistrados.Add(fp);
                    }

                    List<FuncionesXPerfiles> permisos = permisosRegistrados.Where(x => x.id_perfil == id_perfil
                    && x.id_funcion == id_funcion).ToList();
                    if (permisos.Count >= 1) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    Logger log = new();
                    log.RegistrarERROR(ex, "ERROR BUSCANDO PERMISOS");
                    return false;
                }
            }
        }
    }
}
