using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CréateurChemin3D : MonoBehaviour
{
    
    List<int> cotes = new List<int>();
    private int largeur;
    private List<Vector3> listePos; 
    private List<char> relief;
    private int maxCotes, maxTournantsCotes, minDroit;
    private Random gen = new Random();
   // private char tournant = 't';
  
    public List<Vector3> ListePos
    {
        get => listePos;
    }
    public CréateurChemin3D(int Largeur)
    {
        largeur = Largeur;
        listePos = new CréateurCheminComplet(largeur).CheminComplet;
        
        relief = new List<char>(listePos.Count);
        maxCotes = listePos.Count / 5;
        //TrouverTournants();
        CréerCotes();
        VérifierSiListeBonne();
        
    }
   
    private int VérifierPos()
    {
        int pos = gen.Next(1, cotes.Count);
        int fin = cotes[pos];
        cotes.Remove(fin -1 );
        cotes.Remove(fin);
        cotes.Remove(fin + 1);
        return fin;
    }
    private void CréerCotes()
    {
        int compteurTournantsCotes = 0;
        int pos;
        int grandeur = 0;
        int bond = 0;

        for (int i = 2; i < relief.Count - 1; i++)
        {
            cotes.Add(i);
        }
        
        for (int i = 0; i < maxCotes && cotes.Count != 0; i++)
        {
            pos = VérifierPos();
            grandeur = gen.Next(0, 2);

            if (grandeur == 0)
            {
                bond = 20;
            }
            else
            {
                if (grandeur == 1) 
                {
                    bond = 40;
                }
            }
           
            listePos[pos - 1] = new Vector3(listePos[pos - 1].x, bond, listePos[pos - 1].z);
            listePos[pos] = new Vector3(listePos[pos].x, 2 * bond, listePos[pos].z);
            listePos[pos + 1] = new Vector3(listePos[pos + 1].x, bond, listePos[pos + 1].z);

        }
        
    }
    /*private void TrouverTournants()
    {
        for (int i = 1; i < listePos.Count - 1; i++)
        {
            if (listePos[i - 1].x != listePos[i + 1].x && listePos[i-1].y != listePos[i + 1].y)
            {
                relief.Add(tournant);
                relief.Add(tournant);
                relief.Add(tournant);
                i += 2;
            }
            else
            {
                relief.Add(' ');
            }
        }
    }*/

    

    private void VérifierSiListeBonne()
    {
        if (listePos.Count % 4 != 0)
        {
            int compteur = (listePos.Count / 4) * 4 - listePos.Count - 2;

            for (int i = 0; i < compteur; i++)
            {
                listePos.Add(new Vector3(0,0,0));
            }
        }
    }

   




}
