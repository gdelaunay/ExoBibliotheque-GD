namespace ExoBibliothèque_GD;

public class Livre
{
    public string titre { get; set; }
    private string auteur { get; set; }
    private bool disponible { get; set; }

    public Livre(string titre, string auteur, bool disponible)
    { 
        this.titre = titre;
        this.auteur = auteur;
        this.disponible = disponible;
    }
    
    public override string ToString()
    {
        return $"{titre} - {auteur} - Disponible : {disponible}";
    }
}