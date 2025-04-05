

using System.ComponentModel.DataAnnotations;

namespace SPHealthSupportSystem_Repositories.Models;

public partial class PsychologyTheory
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]

    public string Description { get; set; }
    [Required]

    public int TopicId { get; set; }
    [Required]

    public string Author { get; set; }
    [Required]

    public int? YearPublished { get; set; }
    [Required]

    public string TheoryType { get; set; }
    [Required]

    public string Principle { get; set; }
    [Required]

    public string Application { get; set; }
    [Required]

    public string RelatedTheory { get; set; }
    [Required]

    public string Criticism { get; set; }
    [Required]

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Topic? Topic { get; set; }
}