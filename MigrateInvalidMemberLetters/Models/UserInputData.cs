#nullable enable

using MigrateInvalidMemberLetters.Extensions;

namespace MigrateInvalidMemberLetters.Models
{
    internal class UserInputData
    {
        public string SourceConnectionString { get; init; } = string.Empty;

        public string TargetConnectionString { get; init; } = string.Empty;

        public void ThrowErrorIfInvalid() 
        {
            var propertiesWithCheckResult = new List<(string propertyName, bool isNullOrEmpty)>() 
            {
                (nameof(SourceConnectionString), SourceConnectionString.IsNullOrEmptyOrWhitespace()),
                (nameof(TargetConnectionString), TargetConnectionString.IsNullOrEmptyOrWhitespace()),
            };

            if (propertiesWithCheckResult.Any(item => item.isNullOrEmpty)) 
            {
                throw new InvalidOperationException($"Configurations '{string.Join(",", propertiesWithCheckResult.Select(x => x.propertyName))}' cannot be null or empty or empty string.");
            }
        }
    }
}
