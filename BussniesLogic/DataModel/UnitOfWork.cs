using BussniesLogic.DataModel.Reposiroties;
using ObligatorioProgramacion.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniesLogic.DataModel
{
    public class UnitOfWork : IDisposable
    {
        protected readonly Contexto _context;
        protected DbContextTransaction Transaction;

        public PeleadorRepository PeleadorRepository { get; set; }
        public BardoRepository BardoRepository { get; set; }
        public FotosRepository FotosRepository { get; set; }

        public UnitOfWork()
        {
            this._context = new Contexto();
            this.BardoRepository = new BardoRepository(this._context);
            this.PeleadorRepository = new PeleadorRepository(this._context);
            this.FotosRepository = new FotosRepository(this._context);
            
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void BeginTransaction()
        {
            this.Transaction = this._context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (this.Transaction != null)
                this.Transaction.Commit();
        }

        public void Rollback()
        {
            if (this.Transaction != null)
                this.Transaction.Rollback();
        }

        public void Dispose()
        {
            if (this.Transaction != null)
                this.Transaction.Dispose();

            this._context.Dispose();
        }
    }
}
