using Domain.Base.Entities.Connection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get => _unitOfWork; }

        /// <summary>
        /// Función para ejecutar el insert de la entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CreateAsync(T entity)
        {
            try
            {
                await _unitOfWork.DbContext.Set<T>().AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Función para actualizar el registro de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            try
            {
                _unitOfWork.DbContext.Entry(entity).State = EntityState.Modified;
                await _unitOfWork.DbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Función para buscar un registro de acuerdo a la condición dada
        /// </summary>
        /// <param name="WhereCondition"></param>
        /// <returns>El primer registro de la busqueda</returns>
        public IEnumerable<T> FindByAsync(Expression<Func<T, bool>> WhereCondition)
        {
            IQueryable<T> query = _unitOfWork.DbContext.Set<T>();
            query = query.Where(WhereCondition);

            return query.ToList();
        }

        /// <summary>
        /// Función para ejecutar un stored procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp_name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedure<U>(string sp_name, IEnumerable<(string, object)> parameters)
        {
            return ExecuteAndWrapQuery<T>(sp_name, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Función para ejecutar un query mediante sentencias
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<U>(string query, IEnumerable<(string, object)> parameters)
        {
            return ExecuteAndWrapQuery<T>(query, parameters, CommandType.Text);
        }

        private IEnumerable<T> ExecuteAndWrapQuery<U>(string query, IEnumerable<(string, object)> parameters, CommandType commandType)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(query);
                command.Connection = connection;
                command.CommandType = commandType;
                command.CommandTimeout = 3600;

                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(p.Item1, p.Item2));
                    }
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var type = typeof(T);
                List<T> result = new List<T>();

                while (reader.Read())
                {
                    var model = Activator.CreateInstance<T>();

                    foreach (var p in type.GetProperties())
                    {
                        try
                        {
                            Type tipo = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                            var value = reader.GetValue(p.Name);

                            if (value != DBNull.Value)
                                p.SetValue(model, Convert.ChangeType(value, tipo), null);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                    result.Add(model);
                }

                return result;
            }
        }
    }
}
