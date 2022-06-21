using ObligatorioProgramacion.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniesLogic.DataModel.Reposiroties
{
    public class FotosRepository
    {

        private readonly Contexto _context;

        public FotosRepository(Contexto context)
        {
            this._context = context;
        }

        public void AddFoto(Fotos foto)
        {
            this._context.Fotos.Add(foto);
        }

        public List<Fotos> GetFotosById(long id)
        {
            return this._context.Fotos.Where(f => f.IdPeleador == id).ToList();
        }


        public List<Fotos> GetFotos()
        {
            return this._context.Fotos.ToList();
        }


    }
}
