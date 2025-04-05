
namespace SPHealthSupportSystem_GraphQL_Client.Models
{
    public class PsychologyTheory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TopicName { get; set; }
        public int TopicId { get; set; }
        public string Author { get; set; }
        public int? YearPublished { get; set; }
        public string TheoryType { get; set; }
        public string Principle { get; set; }
        public string Application { get; set; }
        public string RelatedTheory { get; set; }
        public string Criticism { get; set; }
    }
}
