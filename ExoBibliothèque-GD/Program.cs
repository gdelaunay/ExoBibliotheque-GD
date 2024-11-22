namespace ExoBibliothèque_GD;

public class Program
{
    static void Main(string[] args)
    {
        Bibliotheque bibliotheque = new Bibliotheque();
        // Si les fichiers n'existent pas : 
        //XmlWriter.Create(fichierUtilisateurs);
        //XmlWriter.Create(fichierLivres);
        //sauvegarderMemoire();
        
        bibliotheque.ChargerMemoire();
        bibliotheque.utilisateurActuel = bibliotheque.MenuConnexion();
        while (bibliotheque.quitter == false)
        {
            bibliotheque.Menu();
        }
    }
}