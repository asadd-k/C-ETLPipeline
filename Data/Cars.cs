namespace ETLPipeline.Data
{
    public record Cars
    {
        public int CarId { get; init; }

        public string? Name { get; init; }
        
        public string? Company { get; init; }
        
        public int Year { get; init; }
        
        public int Price { get; init; }

    }
}
