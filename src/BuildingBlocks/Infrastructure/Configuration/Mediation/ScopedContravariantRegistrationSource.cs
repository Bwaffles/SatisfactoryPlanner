using Autofac.Core;
using Autofac.Features.Variance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Mediation
{
    public class ScopedContravariantRegistrationSource : IRegistrationSource
    {
        private readonly ContravariantRegistrationSource _source = new();
        private readonly List<Type> _types = [];

        public ScopedContravariantRegistrationSource(params Type[] types)
        {
            ArgumentNullException.ThrowIfNull(types, nameof(types));

            if (!types.All(x => x.IsGenericTypeDefinition))
                throw new ArgumentException("Supplied types should be generic type definitions");

            _types.AddRange(types);
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            foreach (var component in _source.RegistrationsFor(service, registrationAccessor))
            {
                var defs = component.Target.Services
                    .OfType<TypedService>()
                    .Select(x => x.ServiceType.GetGenericTypeDefinition());

                if (defs.Any(_types.Contains))
                    yield return component;
            }
        }

        public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
    }
}