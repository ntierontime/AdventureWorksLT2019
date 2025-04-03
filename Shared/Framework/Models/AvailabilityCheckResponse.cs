namespace Framework.Models
{
    public class AvailabilityCheckResponse
    {
        /// <summary>
        /// true when not in database
        /// </summary>
        public bool Availability { get; set; }

        /// <summary>
        /// give suggestions if the name is not available.
        /// </summary>
        public string[]? Suggestions { get; set; }
    }
}

