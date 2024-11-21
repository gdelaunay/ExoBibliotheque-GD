using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace ExoBibliothèque_GD;

public class Bibliotheque
{
    public static List<Livre> livres = new List<Livre>();
    public static bool quitter = false;
    static void Main(string[] args)
    {
        while (quitter == false)
        {
            Menu();
        }
    }

    static void Menu()
    {
        int choix = Question();
        switch (choix)
        {
            case 1:
                AjouterLivre(NouveauLivre());
                break;
            case 2:
                ListerLivres();
                break;
            case 3:
                RechercherLivre();
                break;
            case 4:
                Quitter();
                break;
        }
    }
    static int Question()
    {
        int choix = 0;
        int[] options = [1, 2, 3, 4];

        while (!options.Contains(choix))
        {
            Console.WriteLine("------------------------- Que souhaitez vous faire ? -------------------------");
            Console.WriteLine("- Ajouter un livre [1] ");
            Console.WriteLine("- Lister tous les livres [2] ");
            Console.WriteLine("- Rechercher un livre par titre [3]");
            Console.WriteLine("- Quitter le programme [4]");
            choix = int.Parse(Console.ReadLine() ?? string.Empty);
            if (!options.Contains(choix))
            {
                Console.WriteLine("Je n'ai pas compris votre choix, merci de répondre 1, 2, 3, ou 4");
            }
        }
        return choix;
    }

    static void Quitter()
    {
        quitter = true;
    }
    static void RechercherLivre()
    {
        Console.WriteLine("Quel est le titre du livre que vous voulez rechercher ? ");
        string titreRecherche = Console.ReadLine() ?? string.Empty;
        Livre livreTrouve = livres.Find(livre => livre.titre == titreRecherche);
        if (livreTrouve != null)
        {
            Console.WriteLine($"Livre trouvé : {livreTrouve.ToString()}");
        }
        else
        {
            Console.WriteLine("Pas trouvé de livre avec ce titre!");
        }
        Console.WriteLine(String.Empty);
    }
    static void ListerLivres()
    {
        Console.WriteLine("Bibliothèque :");
        foreach (Livre livre in livres)
        {
            Console.WriteLine(livre.ToString());
        }
        Console.WriteLine(String.Empty);
    }
    static Livre NouveauLivre()
    {
        string titre;
        string auteur;
        bool? disponible = null;
        
        Console.WriteLine("------------------------- Nouveau livre ---------------------------------------");
        Console.Write("Titre : ");
        titre = Console.ReadLine() ?? string.Empty;
        Console.Write("\nAuteur : ");
        auteur = Console.ReadLine() ?? string.Empty;
        while (disponible == null)
        {
            Console.Write("\nDisponible ? (true/false) : ");
            try
            {
                disponible = bool.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            }
            catch (Exception e)
            {
                disponible = null;
                Console.WriteLine("Merci de répondre par \"true\" ou par \"false\".");
            }
        }
        return new Livre(titre, auteur, (bool)disponible);
    }
    static void AjouterLivre(Livre livre)
    {
        livres.Add(livre);
        Console.WriteLine("Livre ajouté : " + livre.ToString() + "\n");
    }
}