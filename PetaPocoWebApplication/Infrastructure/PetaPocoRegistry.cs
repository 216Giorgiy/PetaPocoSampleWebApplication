﻿using PetaPoco;
using StructureMap.Configuration.DSL;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaPocoRegistry : Registry
    {
        public PetaPocoRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                //x.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<>));
                x.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>));
                x.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                x.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<,>));

                x.AddAllTypesOf(typeof (IQueryHandler)).NameBy(y => y.Name.ToLowerInvariant());
            });

            For<IDatabase>().HttpContextScoped().Use(GetDatabase);
            For<IDatabaseQuery>().HttpContextScoped().Use(sm => sm.GetInstance<IDatabase>());
        }

        public static IDatabase GetDatabase()
        {
            return new MyDb("Peta");
        }
    }
}
