namespace ETLPipeline.Data
{
    public record CarReviews
    {
        public int CarId { get; init; }

        public int UserId { get; init; }

        public string? Review { get; init; }
    }
}
