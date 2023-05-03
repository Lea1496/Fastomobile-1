
using System.IO;


public class DataCoin
{
    const string Chemin = "Assets/";
    private StreamWriter fluxÉcriture;
    private StreamReader fluxLecture;
    private int nbCoins = 0;
    public void AjouterCoin(string nomFichier, int coins)
    {

        using (fluxLecture = new StreamReader(Chemin + nomFichier))
        {
            nbCoins = int.Parse(fluxLecture.ReadLine());
        }

        nbCoins += coins;
        using (fluxÉcriture = new StreamWriter(Chemin + nomFichier))
        {
            fluxÉcriture.Write(nbCoins);
        }
    }

    public void EnleverCoin(string nomFichier1, string nomFichier2)
    {
        using (fluxÉcriture = new StreamWriter(Chemin + nomFichier1))
        {
            fluxÉcriture.Write(0);
        }
        using (fluxÉcriture = new StreamWriter(Chemin + nomFichier2))
        {
            fluxÉcriture.Write(0);
        }
    }
    public int AccederNbCoins(string nomFichier)
    {
        int coins;
        using (fluxLecture = new StreamReader(Chemin + nomFichier))
        {
            
            coins = int.Parse(fluxLecture.ReadLine());

        }

        return coins;
    }
}
