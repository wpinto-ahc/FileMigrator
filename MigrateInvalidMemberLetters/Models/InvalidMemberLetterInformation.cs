namespace MigrateInvalidMemberLetters.Models
{
    internal record InvalidMemberLetterInformation
    {
        internal int Id { get; init; }

        internal int InvalidMemberEnrollmentId { get; init; }

        internal string FilePath { get; init; } = string.Empty;

        internal string Language { get; init; } = string.Empty;
    }
}
