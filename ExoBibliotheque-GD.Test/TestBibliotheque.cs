using System.Text.Json.Serialization.Metadata;
using ExoBibliothèque_GD;

namespace ExoBibliotheque_GD.Test;

public class TestBibliotheque
{
    [Fact]
    public void TestAjouterLivre()
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        bibliotheque.AjouterLivre(new Livre("Bonjour", "Goulwen Delaunay", true));
        Assert.True(bibliotheque.livres.Count == 1);
        bibliotheque.AjouterLivre(new Livre("Au revoir", "Goulwen Delaunay", true));
        Assert.True(bibliotheque.livres.Count == 2);
    }
    
    [Fact]
    public void TestNouveauLivre()
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        Livre nouveauLivre = new Livre("Bonjour", "Goulwen Delaunay", true);
        
        var input = new StringReader("Bonjour\nGoulwen Delaunay\nTrue\n");
        Console.SetIn(input);

        Livre? livreCree = bibliotheque.NouveauLivre();
        Assert.NotNull(livreCree);
        Assert.Equivalent(nouveauLivre, livreCree);
    }
    
    [Fact]
    public void TestListerLivres()
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        bibliotheque.AjouterLivre(new Livre("Bonjour", "Goulwen Delaunay", true));
        
        var output = new StringWriter();
        Console.SetOut(output);
        string sortieAttendue = "Bibliothèque :\r\nBonjour - Goulwen Delaunay - Disponible : True\r\n\r\n";
        
        bibliotheque.ListerLivres();
        Assert.Equal(sortieAttendue, output.ToString());
    }
    
    [Fact]
    public void TestRechercherLivre()
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        string titreLivre = "Bonjour";
        Livre nouveauLivre = new Livre(titreLivre, "Goulwen Delaunay", true);
        bibliotheque.AjouterLivre(nouveauLivre);
        
        var input = new StringReader(titreLivre + Environment.NewLine);
        Console.SetIn(input);
        
        Livre? livreTrouve = bibliotheque.RechercherLivre();
        Assert.NotNull(livreTrouve);
        Assert.Equal(livreTrouve, nouveauLivre);
    }
    
    [Fact]
    public void TestEmprunterLivre()
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        string titreLivre = "Bonjour";
        Livre nouveauLivre = new Livre(titreLivre, "Goulwen Delaunay", true);
        bibliotheque.AjouterLivre(nouveauLivre);
        Utilisateur nouvelUtilisateur = new Utilisateur("Goulwen", "goulwen@email.fr");
        bibliotheque.utilisateurActuel = nouvelUtilisateur;
        
        var input = new StringReader(titreLivre + Environment.NewLine);
        Console.SetIn(input);
        
        bibliotheque.EmprunterLivre();
        Assert.True(nouvelUtilisateur.livresEmpruntes.Count == 1);
        Assert.True(nouvelUtilisateur.livresEmpruntes[0] == nouveauLivre);
        Assert.False(bibliotheque.livres[0].disponible);
    }
}