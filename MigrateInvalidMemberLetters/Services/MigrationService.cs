using MigrateInvalidMemberLetters.Models;

namespace MigrateInvalidMemberLetters.Services
{
    internal class MigrationService
    {
        private const string TargetContainerName = "enrollment-letters";
        private const string InvalidMemberPrefixFolders = "invalid-application-portal/enrollment-letters";

        public async Task MigrateLetters(IEnumerable<InvalidMemberLetterInformation> letters, UserInputData userInputData) 
        {
            Console.WriteLine($"Starting migration of {letters.Count()} letters.");
            Console.WriteLine($" ");
            var azureStorageService = new AzureStorageService(userInputData);

            foreach (var letter in letters) 
            {
                try
                {
                    Console.WriteLine($"--> Migrating letter request {letter.Id}...");
                    using (var fileStream = File.OpenRead(letter.FilePath))
                    {
                        var targetFilePath = CreateTargetFilePathFor(letter);
                        var isSuccess = await azureStorageService.UploadFileToContainer(fileStream, TargetContainerName, targetFilePath);
                        var message = isSuccess
                            ? $"----> SUCCESS: Migrated letter {letter.Id} with MI_MMID {letter.InvalidMemberEnrollmentId}."
                            : $"----> FAILED: Could not migrate letter {letter.Id} with MI_MMID {letter.InvalidMemberEnrollmentId}.";

                        Console.WriteLine(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"----> OPERATION FAILED: Could not migrate letter {letter.Id} with MI_MMID {letter.InvalidMemberEnrollmentId}: {ex.Message}");
                }
                finally 
                {
                    Console.WriteLine($"  ");
                }
            }
        }

        private string CreateTargetFilePathFor(InvalidMemberLetterInformation letter) =>
            $"{InvalidMemberPrefixFolders}/{letter.InvalidMemberEnrollmentId}/{Path.GetFileName(letter.FilePath)}";
    }
}
