using BussniesLogic.DataModel;
using BussniesLogic.Services.Seguridad;
using EnumEstadosBardo;
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
        [Route("api/Peleador/AddPeleador")]
        public IHttpActionResult AddPeleador([FromBody] PeleadorModelAgregar peleador)
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
                        Ruta = peleador.Foto,
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

        [HttpPost]
        [Route("api/Peleador/AddFoto")]
        public IHttpActionResult AddFoto([FromBody] FotoModel foto)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var fotoEntity = new Fotos()
                    {
                        IdPeleador = foto.IdPeleador,
                        Ruta = foto.Ruta,
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
        [HttpGet]
        
        [Authorize]
        [Route("api/Peleador/GetPeleadoresByLocalidad")]
        public IHttpActionResult GetPeleadoresByLocalidad([FromBody] PeleadorModel peleadorUsuario)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                try
                {
                    List<Peleador> colPeleadores = uow.PeleadorRepository.GetPeleadoresByLocalidad(peleadorUsuario.IdPeliador , peleadorUsuario.Ciudad);
                    List<PeleadorModel> peleadoresRetornar = new List<PeleadorModel>();
                    foreach(Peleador aux in colPeleadores)
                    {
                        PeleadorModel peleador = new PeleadorModel();
                        peleador.Nombre = aux.Nombre;
                        peleador.Apellido = aux.Apellido;
                        peleador.Resumen = aux.Resumen;
                        peleador.Pais = aux.Pais;
                        peleador.Ciudad = aux.Ciudad;
                        peleador.Fotos = uow.FotosRepository.GetRutasFotosById(aux.IdPeliador);
                        peleadoresRetornar.Add(peleador);
                    }
                    return Ok(peleadoresRetornar);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPost]
        [Route("api/Peleador/HayBardo")]
        public IHttpActionResult HayBardo([FromBody] PeleadorModel peleadorUsuario , long idPeleador2)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    List<Bardos> colBardos = uow.BardoRepository.GetBardos();
                    Bardos bardo = colBardos.Where(b => b.IdPeleadorUno == peleadorUsuario.IdPeliador && b.IdPeleadorDos == idPeleador2 || b.IdPeleadorUno == idPeleador2 && b.IdPeleadorDos == peleadorUsuario.IdPeliador).FirstOrDefault();
                    if (bardo == null)
                    {
                        Bardos aux = new Bardos();
                        aux.IdPeleadorUno = peleadorUsuario.IdPeliador;
                        aux.IdPeleadorDos = idPeleador2;
                        colBardos.Add(aux);
                        long idBardo = uow.BardoRepository.GetIdBardoById(peleadorUsuario.IdPeliador , idPeleador2);
                        EstadoBardos nuevoEstao = new EstadoBardos();
                        nuevoEstao.IdBardo = idBardo;
                        nuevoEstao.Estado = ConstEstadoBardo.PENDIENTE;
                    }
                    else
                    {
                        long idBardo = uow.BardoRepository.GetIdBardoById(peleadorUsuario.IdPeliador, idPeleador2);
                        EstadoBardos nuevoEstao = uow.EstadoBardosRepository.GetEstadoBardosById(idBardo);
                        nuevoEstao.Estado = ConstEstadoBardo.CONCRETADO;
                    }
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
