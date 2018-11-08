namespace RLG.InsuranceUtility.Helpers
{
    public enum PolicyManagementFee
    {
        A = 3, //Policy Type A - Policy was taken out before 01/01/1990
        B = 5, //Policy Type B - Policy has membership rights
        C = 7, //Policy Type C - Policy was taken out on or after 01/01/1990 and has membership rights
    }

    /// <summary>
    /// Determines how empty lines are interpreted when reading CSV files.
    /// These values do not affect empty lines that occur within quoted fields
    /// or empty lines that appear at the end of the input file.
    /// </summary>
    public enum EmptyLineBehavior
    {
        /// <summary>
        /// Empty lines are interpreted as a line with zero columns.
        /// </summary>
        NoColumns,
        /// <summary>
        /// Empty lines are interpreted as a line with a single empty column.
        /// </summary>
        EmptyColumn,
        /// <summary>
        /// Empty lines are skipped over as though they did not exist.
        /// </summary>
        Ignore,
        /// <summary>
        /// An empty line is interpreted as the end of the input file.
        /// </summary>
        EndOfFile,
    }
}