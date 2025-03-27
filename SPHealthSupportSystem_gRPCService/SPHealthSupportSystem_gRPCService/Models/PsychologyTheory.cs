namespace SPHealthSupportSystem_gRPCService.Models;

public partial class PsychologyTheory
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int TopicId { get; set; }

    public string Author { get; set; } = string.Empty;

    public int? YearPublished { get; set; }

    public string TheoryType { get; set; } = string.Empty;

    public string Principle { get; set; } = string.Empty;

    public string Application { get; set; } = string.Empty;

    public string RelatedTheory { get; set; } = string.Empty;

    public string Criticism { get; set; } = string.Empty;

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}