using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace WebTenMeetings.Controllers
{
    internal class BaseGenericRepository
    {
        public class GenericRepository<TContext> : IGenericRepository
       where TContext : DbContext
        {

            string _nomeInstancia;


            protected TContext Contexto { get; private set; }

            #region Pega o mesmo contexto já instanciado na memória

            private void NovoContexto()
            {
                Contexto = (TContext)Activator.CreateInstance(typeof(TContext));
            }
            //private void SetContexto(TContext contexto)
            //{
            //    object value = CallContext.SetData(_nomeInstancia, contexto);
            //}
            //protected TContext GetContexto()
            //{
            //    return (TContext)CallContext.GetData(_nomeInstancia);
            //}

            public GenericRepository()
            {
                _nomeInstancia = typeof(TContext).ToString();

                Contexto = GetContexto();

                if (Contexto == null)
                {
                    NovoContexto();
                    SetContexto(Contexto);
                }
            }

            private void SetContexto(TContext? contexto)
            {
                throw new NotImplementedException();
            }

            private TContext? GetContexto()
            {
                throw new NotImplementedException();
            }

            #endregion

            public GenericRepository(TContext contexto)
            {
                _nomeInstancia = typeof(TContext).ToString();
                Contexto = contexto;
            }





            public List<T> SqlQuery<T>(string sql)
            {
                return Contexto.Database.SqlQuery<T>(sql).ToList();
            }

            public List<T> SqlQuery<T>(string sql, params object[] parametros)
            {
                return Contexto.Database.SqlQuery<T>(sql, parametros).ToList();
            }

            public int ExecuteSqlCommand(string sql, params object[] parametros)
            {
                return Contexto.Database.ExecuteSqlCommand(sql, parametros);
            }

            public DbContextTransaction BeginTransaction()
            {
                return Contexto.Database.BeginTransaction();
            }


            public virtual void Alterar<T>(T instancia) where T : class
            {
                Anexar(instancia, EntityState.Modified);
            }

            public void Anexar<T>(T instancia) where T : class
            {
                Contexto.Set<T>().Attach(instancia);
            }

            public void Anexar<T>(T instancia, EntityState state) where T : class
            {
                Anexar(instancia);
                Contexto.Entry(instancia).State = state;
            }

            public void Anexar<T>(ICollection<T> lista, EntityState state) where T : class
            {
                foreach (var item in lista)
                    Anexar(item, state);

            }

            public void Excluir<T>(List<T> instancia) where T : class
            {
                foreach (var item in instancia)
                {
                    Anexar(item, EntityState.Deleted);
                }
            }

            public void Excluir<T>(T instancia) where T : class
            {
                Anexar(instancia, EntityState.Deleted);
            }
            [Obsolete("Utilizar o RemoveRange")]
            public virtual IEnumerable<T> Excluir<T>(Expression<Func<T, bool>> func) where T : class
            {
                return RemoveRange<T>(func);
            }

            public virtual void Remove<T>(T instancia) where T : class
            {
                Contexto.Set<T>().Remove(instancia);
            }
            public virtual void Remove<T>(Expression<Func<T, bool>> func) where T : class
            {
                Remove(ObterTodos<T>().Where(func).SingleOrDefault());
            }
            public virtual IEnumerable<T> RemoveRange<T>(Expression<Func<T, bool>> func) where T : class
            {
                return Contexto.Set<T>().RemoveRange(ObterTodos<T>().Where(func));
            }





            public void Incluir<T>(ICollection<T> instancia) where T : class
            {
                Contexto.Set<T>().AddRange(instancia);
            }
            public void Incluir<T>(List<T> instancia) where T : class
            {
                Contexto.Set<T>().AddRange(instancia);
            }

            public void Incluir<T>(T instancia) where T : class
            {
                Contexto.Set<T>().Add(instancia);
            }

            public int Salvar()
            {
                return Contexto.SaveChanges();
            }




            //public void Dispose()
            //{
            //    Contexto.Dispose();
            //    object value = CallContext.FreeNamedDataSlot(_nomeInstancia);
            //}

            public IQueryable<T> ObterTodos<T>() where T : class
            {
                return Contexto.Set<T>();
            }




            public void PropriedadeParaAlterar<T, TProperty>(T instancia, Expression<Func<T, TProperty>> property, bool ismodified = true) where T : class
            {
                Propriedade(instancia, property).IsModified = ismodified;
            }

            public DbPropertyEntry<T, TProperty> Propriedade<T, TProperty>(T instancia, Expression<Func<T, TProperty>> property) where T : class
            {
                return Contexto.Entry(instancia).Property(property);
            }
        }


        public class GenericRepository<T, TContext> : GenericRepository<TContext>, IGenericRepository<T>
            where T : class
            where TContext : DbContext
        {
            public GenericRepository() : base() { }

            public GenericRepository(TContext contexto) : base(contexto) { }



            public virtual IQueryable<T> ObterTodos()
            {
                return Contexto.Set<T>();
            }



            public IGenericRepository<T> Incluir(T instancia)
            {
                Contexto.Set<T>().Add(instancia);

                return this;
            }
            public IGenericRepository<T> Incluir(ICollection<T> instancia)
            {
                Contexto.Set<T>().AddRange(instancia);

                return this;
            }



            public virtual IGenericRepository<T> Alterar(T instancia)
            {
                Anexar(instancia, EntityState.Modified);

                return this;
            }



            public virtual IGenericRepository<T> Excluir(T instancia)
            {
                Anexar(instancia, EntityState.Deleted);
                return this;
            }
            public virtual IGenericRepository<T> Excluir(List<T> instancia)
            {
                foreach (var item in instancia)
                {
                    Anexar(instancia, EntityState.Deleted);
                }

                return this;
            }
            public virtual IEnumerable<T> Excluir(Expression<Func<T, bool>> func)
            {
                return Contexto.Set<T>().RemoveRange(ObterTodos().Where(func));
            }




            public virtual IGenericRepository<T> Anexar(T instancia)
            {
                Contexto.Set<T>().Attach(instancia);

                return this;
            }

            public virtual IGenericRepository<T> Anexar(ICollection<T> lista, EntityState state)
            {
                foreach (var item in lista)
                    Anexar(item, state);

                return this;
            }

            public T Find(int id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<T> List()
            {
                throw new NotImplementedException();
            }

            public void Add(T item)
            {
                throw new NotImplementedException();
            }

            public void Remove(T item)
            {
                throw new NotImplementedException();
            }

            public void Edit(T item)
            {
                throw new NotImplementedException();
            }
        }



    }

    public interface IGenericRepository
    {
    }
}