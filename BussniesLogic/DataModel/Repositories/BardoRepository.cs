using ObligatorioProgramacion.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniesLogic.DataModel.Reposiroties
{
    public class BardoRepository
    {
        private readonly Contexto _context;

        public BardoRepository(Contexto context)
        {
            this._context = context;
        }

        public void AddBardo(Bardos bardo)
        {
            this._context.Bardos.Add(bardo);
        }

        public List<Bardos> GetBardosById(long id)
        {
            return this._context.Bardos.Where(p => p.IdPeleadorUno == id).ToList();
        }

        public List<Bardos> GetBardos()
        {
            return this._context.Bardos.ToList();
        }


        public void RemoveBardo(Bardos bardo)
        {
            this._context.Bardos.Remove(bardo);
        }
    }
}
