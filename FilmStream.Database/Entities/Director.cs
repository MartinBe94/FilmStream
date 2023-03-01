namespace FilmStream.Database.Entities;
public class Director:IEntity
{
    public Director()
    {
        Films = new HashSet<Film>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Film>? Films { get; set; }
}
