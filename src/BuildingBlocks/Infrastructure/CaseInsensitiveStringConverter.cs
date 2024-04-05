using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure
{
    public class CaseInsensitiveStringConverter : ValueConverter<CaseInsensitiveString, string>
    {
        public CaseInsensitiveStringConverter()
            : base(caseInsensitiveString => caseInsensitiveString.Value, value => new CaseInsensitiveString(value)) { }
    }
}
