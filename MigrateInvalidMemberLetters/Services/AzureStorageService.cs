#nullable enable

using Azure.Storage.Blobs;
using MigrateInvalidMemberLetters.Models;

namespace MigrateInvalidMemberLetters.Services
{
    internal class AzureStorageService
    {
        private readonly string connectionString;

        public AzureStorageService(UserInputData userInputData)
        {
            connectionString = userInputData.TargetConnectionString;
        }

        public async Task<bool> UploadFileToContainer(FileStream fileToUpload, string containerName, string filePathToSave) 
        {
            ArgumentNullException.ThrowIfNull(fileToUpload, nameof(fileToUpload));
            ArgumentNullException.ThrowIfNullOrEmpty(containerName, nameof(containerName));
            ArgumentNullException.ThrowIfNullOrEmpty(filePathToSave, nameof(filePathToSave));

            try
            {
                // Create a BlobServiceClient
                var blobServiceClient = new BlobServiceClient(connectionString);

                // Get container
                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Get a reference to a blob
                var blobClient = containerClient.GetBlobClient(filePathToSave);

                // Upload the file
                var result = await blobClient.UploadAsync(fileToUpload, true);

                fileToUpload.Close();

                return result.GetRawResponse().IsError;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"OPERATION FAILED: Could not upload file '{Path.GetFileName(filePathToSave)}'. Error: {ex.Message}");
                return false;
            }
        }
    }
}
