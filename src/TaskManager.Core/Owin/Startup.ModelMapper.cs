using AutoMapper;
using LightInject;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using TaskManager.Core.Storage.Entities;
using TaskManagerDemo.Core.Platform.IoC;
using TaskManagerDemo.Core.Platform.IoC.Extensions;
using TaskManagerDemo.Core.Mvc.WebApi.ViewModels.Tasks;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork;
using TaskManagerDemo.Core.Storage.Orm.UnitOfWork.NHibernate;
using TaskManagerDemo.Core.Storage.Repositories.Common;

namespace TaskManager.Core.Owin
{
    public partial class Startup
    {
        public void ConfigureModelMapper(IAppBuilder app)
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Task, TaskViewModel>();
                config.CreateMap<TaskViewModel, Task>()
                    .ForMember(d => d.Author, m => m.MapFrom(s => new User { Id = s.AuthorId??0 } ));
                config.CreateMap<TaskQuery, IQueryable<Task>>().ConvertUsing((e,q) =>
                    {
                        //Маппинг поискового запроса
                        //TODO: Маппинг должен выполнятся по соглашениям. Релизовать базовый маппер для соглашения 
                        //      Поле=значение => .Where(x => x.Поле = значение) 
                        //      Рефлексией доставать поля, с помощью RunSharp генерировать преобразование и кешировать его
                        if(!String.IsNullOrEmpty(e.Name))
                        {
                            q = q.Where(x => x.Name.Contains(e.Name)); 
                        }
                        if (e.AuthorId.HasValue)
                        {
                            q = q.Where(x => x.Author.Id == e.AuthorId);
                        }
                        return q;
                    });
            });
        }
    }
}
