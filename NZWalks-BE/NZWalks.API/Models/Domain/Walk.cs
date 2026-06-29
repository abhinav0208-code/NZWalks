namespace NZWalks.API.Models.Domain
{
    public class Walk
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string WalkImageUrl { get; set; }

        public Guid RegionId { get; set; }

        public Guid DifficultyId { get; set; }

        //Navigation properties - (Establish relationship between Walk table and Difficulty and Region table )
        public Difficulty Difficulty { get; set; }

    }
}
