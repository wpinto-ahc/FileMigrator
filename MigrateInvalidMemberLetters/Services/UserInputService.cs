using MigrateInvalidMemberLetters.Models;

namespace MigrateInvalidMemberLetters.Services
{
    internal class UserInputService
    {
        internal UserInputData GetInputData() 
        {
            Console.WriteLine("EzMember's db connection string: ");
            var dbConnectionString = Console.ReadLine();

            Console.WriteLine("AzureStorage's connection string: ");
            var targetStorageConnectionString = Console.ReadLine();

            return new()
            {
                SourceConnectionString = dbConnectionString ?? string.Empty,
                TargetConnectionString = targetStorageConnectionString ?? string.Empty,
            };
        }
    }
}
