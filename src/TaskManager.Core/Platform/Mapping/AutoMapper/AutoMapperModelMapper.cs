using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.Mapping.AutoMapper
{
    public class AutoMapperModelMapper
        : IModelMapper
    {
        public TDest Map<TSource, TDest>(TSource source)
        {
            return Mapper.Map<TSource, TDest>(source);
        }

        public TDest Map<TSource, TDest>(TSource source, TDest dest)
        {
            return Mapper.Map(source, dest);
        }


        public IQueryable<TProjection> ProjectTo<TEntity, TProjection>(IQueryable<TEntity> query)
        {
            return query.ProjectTo<TProjection>();
        }
    }
}
