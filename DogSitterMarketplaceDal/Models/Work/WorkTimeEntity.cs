namespace DogSitterMarketplaceDal.Models.Work
{
    public class WorkTimeEntity
    {
        public int Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly Stop { get; set; }
    }
}
