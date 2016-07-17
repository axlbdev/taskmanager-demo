using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Mapping
{
    /// <summary>
    /// Маппер
    /// </summary>
    public interface IModelMapper
    {
        /// <summary>
        /// Создать TDest и замапить в него TSource
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDest Map<TSource, TDest>(TSource source);
        /// <summary>
        /// Замапить TSource в TDest
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        TDest Map<TSource, TDest>(TSource source, TDest dest);
        /// <summary>
        /// Построить проекцию для TProjection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>

        IQueryable<TProjection> ProjectTo<TEntity, TProjection>(IQueryable<TEntity> query);
    }
}
