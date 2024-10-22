#nullable enable

using MigrateInvalidMemberLetters.Models;
using System.Data.SqlClient;
using Dapper;

namespace MigrateInvalidMemberLetters.Services
{
    internal static class EzMemberService
    {
        internal static async Task<IEnumerable<InvalidMemberLetterInformation>> GetExistingLetterInformations(UserInputData userInputData) 
        {
            try
            {
                using var connection = new SqlConnection(userInputData.SourceConnectionString);
                connection.Open();

                var query = @"
                    SELECT [Id],
                           [MI_MIID] AS InvalidMemberEnrollmentId,
                           [Language], 
                           [FileName] AS FilePath
                    FROM [EZ_Member].[dbo].[MembInvalidLetetrGenRequests]
                    WHERE FileName LIKE '\\%'";

                return await connection.QueryAsync<InvalidMemberLetterInformation>(query);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"OPERATION FAILED: Could not get InvalidMember's letters' information from DB. Error: {ex.Message}");
                throw;
            }
        }
    }
}
