using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sideTrade.myProfilo.webApi
{
    public class EntityMapper<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapper()
        {
            Mapper.CreateMap<Models.Profile, Dal.Profile>();
            Mapper.CreateMap<Dal.Profile, Models.Profile>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperFileManager<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperFileManager()
        {
            Mapper.CreateMap<Models.FileManager, Dal.FileManager>();
            Mapper.CreateMap<Dal.FileManager, Models.FileManager>();

            Mapper.CreateMap<Models.FileManagerType, Dal.FileManagerType>();
            Mapper.CreateMap<Dal.FileManagerType, Models.FileManagerType>();

            Mapper.CreateMap<Models.Profile, Dal.Profile>();
            Mapper.CreateMap<Dal.Profile, Models.Profile>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }


    public class EntityMapperFileManagerType<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperFileManagerType()
        {
            Mapper.CreateMap<Models.FileManagerType, Dal.FileManagerType>();
            Mapper.CreateMap<Dal.FileManagerType, Models.FileManagerType>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperNotification<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperNotification()
        {
            Mapper.CreateMap<Models.Notification, Dal.Notification>();
            Mapper.CreateMap<Dal.Notification, Models.Notification>();

            Mapper.CreateMap<Models.NotificationType, Dal.NotificationType>();
            Mapper.CreateMap<Dal.NotificationType, Models.NotificationType>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperNotificationType<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperNotificationType()
        {
            Mapper.CreateMap<Models.NotificationType, Dal.NotificationType>();
            Mapper.CreateMap<Dal.NotificationType, Models.NotificationType>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperLogin<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperLogin()
        {
            Mapper.CreateMap<Models.Login, Dal.Login>();
            Mapper.CreateMap<Dal.Login, Models.Login>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperProfileRole<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperProfileRole()
        {
            Mapper.CreateMap<Models.ProfileMapping, Dal.ProfileMapping>();
            Mapper.CreateMap<Dal.ProfileMapping, Models.ProfileMapping>();
        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperLogManager<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperLogManager()
        {
            Mapper.CreateMap<Models.Log, Dal.Log>();
            Mapper.CreateMap<Dal.Log, Models.Log>();


            Mapper.CreateMap<Models.LogType, Dal.LogType>();
            Mapper.CreateMap<Dal.LogType, Models.LogType>();

            Mapper.CreateMap<Models.Profile, Dal.Profile>();
            Mapper.CreateMap<Dal.Profile, Models.Profile>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperLogManagerType<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperLogManagerType()
        {
            Mapper.CreateMap<Models.LogType, Dal.LogType>();
            Mapper.CreateMap<Dal.LogType, Models.LogType>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }

    public class EntityMapperSettings<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapperSettings()
        {
            Mapper.CreateMap<Models.Settings, Dal.Settings>();
            Mapper.CreateMap<Dal.Settings, Models.Settings>();

        }

        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }
}
  