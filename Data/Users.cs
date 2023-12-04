namespace ETLPipeline.Data
{
    public record Users
    {
        public int Id { get; init; }

        public string? UserName { get; init; }
        
        public string? Email { get; init; }
    }
}
