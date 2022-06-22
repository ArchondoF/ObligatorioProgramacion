using ObligatorioProgramacion.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniesLogic.DataModel.Repositories
{
    public class EstadoBardosRepository
    {

        private readonly Contexto _context;

        public EstadoBardosRepository(Contexto context)
        {
            this._context = context;
        }

        public void AddEstadoBardo(EstadoBardos estadoBardo)
        {
            this._context.EstadoBardos.Add(estadoBardo);
        }

        public List<EstadoBardos> GetEstadoBardosById(long id)
        {
            return this._context.EstadoBardos.Where(f => f.IdBardo == id).ToList();
        }


        public List<EstadoBardos> GetEstadoBardos()
        {
            return this._context.EstadoBardos.ToList();
        }
    }
}
