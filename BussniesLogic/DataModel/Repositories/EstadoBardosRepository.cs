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



        public Estados GetEstadoBardosById(string id)
        {
            return this._context.Estados.Where(p => p.Id == id).FirstOrDefault();
        }

    }
}
