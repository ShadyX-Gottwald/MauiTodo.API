using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
//using Supabase.Postgrest.Attributes;
//using System.ComponentModel.DataAnnotations.Schema;

namespace MauiTodo.API.Models;

[Table("newsletter")]
public class Newsletter: BaseModel
{
    [PrimaryKey("id",false)]
    public long Id { get; set; }
    [Supabase.Postgrest.Attributes.Column("name")]
    public string Name { get; set; }
    [Supabase.Postgrest.Attributes.Column("description")]

    public string Description { get; set; }
    [Supabase.Postgrest.Attributes.Column("read_time")]

    public int  ReadTime { get; set; }
    [Supabase.Postgrest.Attributes.Column("created_at")]

    public DateTime CreatedAt { get; set; }
}
