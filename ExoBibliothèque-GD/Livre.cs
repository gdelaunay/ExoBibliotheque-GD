namespace ExoBibliothèque_GD;

public class Livre
{
    public string titre { get; set; }
    public string auteur { get; set; }
    public bool disponible { get; set; }
    
    public Livre() { }

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