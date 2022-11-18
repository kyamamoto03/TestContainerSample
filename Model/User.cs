using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

[Table("user_data")]
public class User
{
    [Key]
    [Column("id")]
    public string Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

}