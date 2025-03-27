using System.ComponentModel.DataAnnotations;

namespace SPHealthSupportSystem_MVCWebApp.Models;

public partial class PsychologyTheory
{
    [Required]
    public int Id { get; set; }
    [Required]

    public string Name { get; set; } = string.Empty;
    [Required]

    public string Description { get; set; } = string.Empty;
    [Required]

    public int TopicId { get; set; }
    [Required]

    public string Author { get; set; } = string.Empty;
    [Required]

    public int? YearPublished { get; set; }
    [Required]

    public string TheoryType { get; set; } = string.Empty;
    [Required]

    public string Principle { get; set; } = string.Empty;
    [Required]

    public string Application { get; set; } = string.Empty;
    [Required]

    public string RelatedTheory { get; set; } = string.Empty;
    [Required]

    public string Criticism { get; set; } = string.Empty;

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}