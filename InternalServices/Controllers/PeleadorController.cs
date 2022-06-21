using BussniesLogic.DataModel;
using BussniesLogic.Services.Seguridad;
using InternalServices.Models;
using ObligatorioProgramacion.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternalServices.Controllers
{
    public class PeleadorController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult AddPeleador([FromBody] PeleadorModel peleador)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    SecurityServices securityService = new SecurityServices();

                    //Genrar SALT
                    string salt = securityService.GenerarSalt(10);

                    //Hashear password usuario
                    string hashedPassword = securityService.GenerarHashSHA256(peleador.Password, salt);


                    //Crear usuario
                    var peleadorEntity = new Peleador()
                    {
                        Nombre = peleador.Nombre,
                        Apellido = peleador.Apellido,
                        Correo = peleador.Correo,
                        Password = hashedPassword,
                        PasswordSalt = salt,
                        Resumen = peleador.Resumen,
                        Pais = peleador.Pais,
                        Ciudad = peleador.Ciudad,
                        
                    };

                    uow.PeleadorRepository.AddPeleador(peleadorEntity);
                    long idPeleador = uow.PeleadorRepository.GetIdPeleadorByCorreo(peleador.Correo);
                    var fotoEntity = new Fotos()
                    {
                        IdPeleador = idPeleador,
                        Ruta = peleador.Foto
                    };
                    uow.FotosRepository.AddFoto(fotoEntity);

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok();
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }
    }
}
