using MigrateInvalidMemberLetters.Services;

namespace MigrateInvalidMemberLetters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInputData = new UserInputService().GetInputData();
            var migrationService = new MigrationService();

            Console.WriteLine("  ");
            Console.WriteLine("Validating inputs...");
            userInputData.ThrowErrorIfInvalid();
            Console.WriteLine("Validated!");
            Console.WriteLine("  ");

            // Get letter's information from DB
            Console.WriteLine("Getting existing letters from DB...");
            var lettersInformation = EzMemberService
                .GetExistingLetterInformations(userInputData)
                .GetAwaiter()
                .GetResult();
            Console.WriteLine("Letters loaded!");
            Console.WriteLine("  ");

            migrationService.MigrateLetters(lettersInformation, userInputData).GetAwaiter().GetResult();
            
            Console.WriteLine("---------------------------");
            Console.WriteLine("  ");
            Console.WriteLine("ALL DONE!");
        }
    }
}
