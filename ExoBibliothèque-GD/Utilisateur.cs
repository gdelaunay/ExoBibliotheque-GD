namespace ExoBibliothèque_GD;

public class Utilisateur
{
    public string nom { get; set; }
    public string email { get; set; }
    public List<Livre> livresEmpruntes { get; set; }
    public static int limiteEmprunts  { get; set; } = 3;

    public Utilisateur(string nom, string email)
    {
        this.nom = nom;
        this.email = email;
        this.livresEmpruntes = new List<Livre>(); 
    }
    
    public void emprunterLivre(Livre livre)
    {
        if (livresEmpruntes.Count < limiteEmprunts)
        {
            livresEmpruntes.Add(livre);
            livre.disponible = false;
            Console.WriteLine($"{nom} a emprunté le livre : {livre}");
        }
        else
        {
            Console.WriteLine("Vous avez déjà emprunté 3 livres, vous ne pouvez pas en emprunter plus.");
        }
    }
}