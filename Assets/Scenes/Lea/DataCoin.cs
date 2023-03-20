using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
            if (int.Parse(fluxLecture.ReadLine()) != null)
            {
                nbCoins = int.Parse(fluxLecture.ReadLine());
            }
            
        }

        nbCoins += coins;
        using (fluxÉcriture = new StreamWriter(Chemin + nomFichier))
        {
            fluxÉcriture.Write(nbCoins);
        }
    }

    public int AccederNbCoins(string nomFichier)
    {
        int coins;
        using (fluxLecture = new StreamReader(Chemin + nomFichier))
        {
            if (int.Parse(fluxLecture.ReadLine()) != null)
            {
                coins = int.Parse(fluxLecture.ReadLine());
            }
            else
            {
                coins = 0;
            }
            
        }

        return coins;
    }
}
