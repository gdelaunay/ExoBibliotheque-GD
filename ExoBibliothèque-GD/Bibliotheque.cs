using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace ExoBibliothèque_GD;

public class Bibliotheque
{
    public List<Livre> livres;
    public List<Utilisateur> utilisateurs;
    public Utilisateur utilisateurActuel;
    public bool quitter;
    public string cheminDossier;
    public string fichierUtilisateurs;
    public string fichierLivres;

    public Bibliotheque()
    {
        livres = new List<Livre>();
        utilisateurs = new List<Utilisateur>();
        quitter = false;
        cheminDossier = "C:\\Users\\Goulwen\\RiderProjects\\ExoBibliothèque-GD\\ExoBibliothèque-GD";
        fichierUtilisateurs = Path.Combine(cheminDossier, "utilisateurs.xml");
        fichierLivres = Path.Combine(cheminDossier, "livres.xml");
    }
   /* static void Main(string[] args)
    {
        // Si les fichiers n'existent pas : 
        //XmlWriter.Create(fichierUtilisateurs);
        //XmlWriter.Create(fichierLivres);
        //sauvegarderMemoire();
        string test = "testdebug";
        ChargerMemoire();
        utilisateurActuel = MenuConnexion();
        Console.WriteLine(utilisateurActuel);
        while (quitter == false)
        {
            Menu();
        }
    }
*/
   public void SauvegarderMemoire()
    {
        using (Stream stream = File.Open(fichierLivres, FileMode.Open))
        {
            var xmlSerializer = new XmlSerializer(livres.GetType());
            xmlSerializer.Serialize(stream, livres);
        }
        using (Stream stream = File.Open(fichierUtilisateurs, FileMode.Open))
        {
            var xmlSerializer = new XmlSerializer(utilisateurs.GetType());
            xmlSerializer.Serialize(stream, utilisateurs);
        }
    }
    public void ChargerMemoire()
    {
        using (Stream stream = File.Open(fichierLivres, FileMode.Open))
        {
            var xmlSerializer = new XmlSerializer(livres.GetType());
            livres = (List<Livre>)xmlSerializer.Deserialize(stream);
        }
        using (Stream stream = File.Open(fichierUtilisateurs, FileMode.Open))
        {
            var xmlSerializer = new XmlSerializer(utilisateurs.GetType());
            utilisateurs = (List<Utilisateur>)xmlSerializer.Deserialize(stream);
        }
    }
    public Utilisateur MenuConnexion()
    {
        Utilisateur utilisateurTrouve = null;
        
        int choix = 0;
        int[] options = [1, 2];
        while (!options.Contains(choix))
        {
            Console.WriteLine("--------------------------------- Connexion ----------------------------------");
            Console.WriteLine("[1] Se connecter");
            Console.WriteLine("[2] Créer un compte");
            choix = int.Parse(Console.ReadLine() ?? string.Empty);
            if (!options.Contains(choix))
            {
                Console.WriteLine("Je n'ai pas compris votre choix, merci de répondre 1 ou 2");
            }
        }

        if (choix == 1)
        {
            Console.WriteLine("Nom d'utilisateur :");
            string nomUtilisateur = Console.ReadLine() ?? string.Empty;
            utilisateurTrouve = utilisateurs.Find(utilisateur => utilisateur.nom == nomUtilisateur);
            if (utilisateurTrouve == null)
            {
                Console.WriteLine("L'utilisateur n'existe pas, veuillez créer un compte.");
                choix = 2;
            }
        } 
        
        if (choix == 2)
        {
            Console.WriteLine("------------------------- Création d'un utilisateur --------------------------");
            Console.WriteLine("Nom utilisateur :");
            string nom = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Email :");
            string email = Console.ReadLine() ?? string.Empty;
            utilisateurTrouve = new Utilisateur(nom, email);
            utilisateurs.Add(utilisateurTrouve);
        };
        
        return utilisateurTrouve;
    }
    public void Menu()
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
                EmprunterLivre();
                break;
            case 5:
                Quitter();
                break;
        }
    }
    public int Question()
    {
        int choix = 0;
        int[] options = [1, 2, 3, 4, 5];

        while (!options.Contains(choix))
        {
            Console.WriteLine("------------------------- Que souhaitez vous faire ? -------------------------");
            Console.WriteLine("- Ajouter un livre [1] ");
            Console.WriteLine("- Lister tous les livres [2] ");
            Console.WriteLine("- Rechercher un livre par titre [3]");
            Console.WriteLine("- Emprunter un livre [4]");
            Console.WriteLine("- Quitter le programme [5]");
            choix = int.Parse(Console.ReadLine() ?? string.Empty);
            if (!options.Contains(choix))
            {
                Console.WriteLine("Je n'ai pas compris votre choix, merci de répondre 1, 2, 3, 4, ou 5");
            }
        }
        return choix;
    }

    public void Quitter()
    {
        SauvegarderMemoire();
        quitter = true;
    }
    public void EmprunterLivre()
    {
        Console.WriteLine("Quel est le titre du livre que vous voulez emprunter ? ");
        string titreRecherche = Console.ReadLine() ?? string.Empty;
        Livre livreTrouve = livres.Find(livre => livre.titre == titreRecherche);
        if (livreTrouve != null)
        {
            Console.WriteLine($"Livre trouvé : {livreTrouve.ToString()}");
            utilisateurActuel.emprunterLivre(livreTrouve);
            Console.WriteLine("Le livre a bien été emprunté");
        }
        else
        {
            Console.WriteLine("Pas trouvé de livre avec ce titre!");
        }
        Console.WriteLine(String.Empty);
    }
    Livre RechercherLivre()
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
        return livreTrouve;
    }
    public void ListerLivres()
    {
        Console.WriteLine("Bibliothèque :");
        foreach (Livre livre in livres)
        {
            if (livre.disponible)
            {
                Console.WriteLine(livre.ToString());
            }
        }
        Console.WriteLine("utilisateurs :");
        foreach (Utilisateur utilisateur in utilisateurs)
        {
            Console.WriteLine(utilisateur.nom);
            
        }
        Console.WriteLine(String.Empty);
    }
    public Livre NouveauLivre()
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
        return new Livre(titre, auteur, disponible.Value);
    }
    public void AjouterLivre(Livre livre)
    {
        livres.Add(livre);
        Console.WriteLine("Livre ajouté : " + livre.ToString() + "\n");
    }
}