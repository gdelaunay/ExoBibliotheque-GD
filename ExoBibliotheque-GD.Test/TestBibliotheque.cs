using ExoBibliothèque_GD;

namespace ExoBibliotheque_GD.Test;

public class TestBibliotheque
{
    [Fact]
    public void Test1()
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        bibliotheque.AjouterLivre(new Livre("Bonjour", "Goulwen Delaunay", true));
        Assert.True(bibliotheque.livres.Count == 1);
    }
}