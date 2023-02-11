using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CréateurChemin3D : MonoBehaviour
{

    private int largeur;
    private List<Vector3> listePos; 
    private List<char> relief;
    private int maxCotes, maxTournantsCotes, minDroit;
    private Random gen = new Random();
    private char tournant = 't';
    private char côte = 'c';
    private char côteTournant = 'e';
    private char droit = 'd';
    
    public CréateurChemin3D(int Largeur, List<Vector3> liste)
    {
        largeur = Largeur;
        listePos = liste;
        relief = new List<char>(listePos.Count);
        TrouverTournants();
        maxCotes = liste.Count / 5;
    }

    private int VérifierPos(List<int> derniersPos)
    {
        int pos = gen.Next(2, listePos.Count);
        if (derniersPos.Contains(pos))
        {
            pos = VérifierPos(derniersPos);
        }
        for (int i = 0; i < derniersPos.Count; i++)
        {
            if (derniersPos[i] - 2 == pos || derniersPos[i] - 1 == pos || derniersPos[i] + 1 == pos || derniersPos[i] + 2 == pos)
            {
                pos = VérifierPos(derniersPos);
            }

        }

        return pos;
    }
    private void CréerCotes()
    {
        int compteurTournantsCotes = 0;
        int pos;
        
        List<int> derniersPos = new List<int>();
        for (int i = 0; i < maxCotes; i++)
        {
            pos = VérifierPos(derniersPos);
            
            derniersPos.Add(pos);
            derniersPos.Add(pos - 1);
            derniersPos.Add(pos + 1);
            if (relief[pos] != ' ')
            {
                compteurTournantsCotes++;
                relief[pos] = côteTournant;
                relief[pos - 1] = côteTournant;
                relief[pos + 1] = côteTournant;
            }
            else
            {
                relief[pos] = côte;
                relief[pos - 1] = côte;
                relief[pos + 1] = côte;
            }
            
        }
        for (int i = 0; i < relief.Count; i++)
        {
            if (relief[i] == ' ')
            {
                relief[i] = droit;
            }
        }
    }
    private void TrouverTournants()
    {
        for (int i = 1; i < listePos.Count; i++)
        {
            if (listePos[i - 1].x != listePos[i + 1].x && listePos[i-1].y != listePos[i + 1].y || relief[i - 1] == tournant || relief[i + 1] == tournant)
            {
                relief.Add(tournant);
            }
            else
            {
                relief.Add(' ');
            }
        }
    }
    
    

}
