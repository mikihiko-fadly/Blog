using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }
    public int Likes { get; set; }
}
