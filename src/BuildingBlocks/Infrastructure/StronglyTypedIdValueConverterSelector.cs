using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure
{
    /// <summary>
    /// Based on https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/
    /// </summary>
    public class StronglyTypedIdValueConverterSelector : ValueConverterSelector
    {
        private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters
            = new();

        public StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
        {
            var baseConverters = base.Select(modelClrType, providerClrType);
            foreach (var converter in baseConverters)
            {
                yield return converter;
            }

            var underlyingProviderType = UnwrapNullableType(providerClrType);
            var underlyingModelType = UnwrapNullableType(modelClrType);

            if (underlyingProviderType is null || underlyingProviderType == typeof(Guid))
            {
                if (underlyingModelType is not null && typeof(TypedIdValueBase).IsAssignableFrom(underlyingModelType))
                {
                    yield return _converters.GetOrAdd((underlyingModelType, typeof(Guid)), _ =>
                    {
                        var converterType = typeof(TypedIdValueConverter<>).MakeGenericType(underlyingModelType);

                        return new ValueConverterInfo(
                            modelClrType: modelClrType,
                            providerClrType: typeof(Guid),
                            factory: valueConverterInfo => (ValueConverter)(Activator.CreateInstance(converterType, valueConverterInfo.MappingHints))!);
                    });
                }
            }
        }

        private static Type? UnwrapNullableType(Type? type)
        {
            if (type is null)
                return null;

            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
