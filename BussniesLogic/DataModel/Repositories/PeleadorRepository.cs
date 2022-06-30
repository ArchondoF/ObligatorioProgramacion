using ObligatorioProgramacion.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniesLogic.DataModel.Reposiroties
{
    public class PeleadorRepository
    {

        private readonly Contexto _context;

        public PeleadorRepository(Contexto context)
        {
            this._context = context;
        }

        public void AddPeleador(Peleador peleador)
        {
            this._context.Peleador.Add(peleador);
        }

        public Peleador GetPeleadorById(long id)
        {
            return this._context.Peleador.FirstOrDefault(p => p.IdPeliador == id);
        }
        

         public List<Peleador> GetPeleadoresByLocalidad(long id , string localidad)
        {
            return this._context.Peleador.Where(p => p.IdPeliador != id && p.Ciudad == localidad).ToList();
        }
        public Peleador GetPeleadorByCorreo(string Correo)
        {
            return this._context.Peleador.FirstOrDefault(p => p.Correo == Correo);
        }
        public long GetIdPeleadorByCorreo(string Correo)
        {
            return this._context.Peleador.Where(p => p.Correo == Correo).Select(p => p.IdPeliador).FirstOrDefault();
        }

        public List<Peleador> GetPeleadores()
        {
            return this._context.Peleador.ToList();
        }


        public void RemovePeleador(Peleador peleador)
        {
            this._context.Peleador.Remove(peleador);
        }
    }
}
