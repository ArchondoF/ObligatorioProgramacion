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
            return this._context.Bardos.Where(p => p.IdPeleadorUno == id || p.IdPeleadorDos == id).ToList();
        }
        public long GetIdBardoByPeleadores(long peleador1 , long peleador2)
        {
            Bardos bardo= this._context.Bardos.Where(b => b.IdPeleadorUno == peleador1 && b.IdPeleadorDos == peleador2 || b.IdPeleadorUno == peleador2 && b.IdPeleadorDos == peleador1).FirstOrDefault();
            return bardo.IdBardo;
        }

        public List<Bardos> GetBardos()
        {
            return this._context.Bardos.ToList();
        }


    }
}
