using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenusData
{
    const string Chemin = "Assets/";
    private StreamWriter fluxÉcriture;
    private StreamReader fluxLecture;
    
    public char AfficherInfo(string nomFichier)
    {
        char info;
        using (fluxLecture = new StreamReader(Chemin + nomFichier))
        {
            info = Char.Parse(fluxLecture.ReadLine());
        }

        return info;
    }

    public void ChangerInfo(string nomFichier, char info)
    {
        using (fluxÉcriture = new StreamWriter(Chemin + nomFichier))
        {
            fluxÉcriture.Write(info);
        }
    }
}
