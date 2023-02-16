using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using Random = System.Random;

public class MarchePas : Exception { }
public class CréateurChemin2D : MonoBehaviour
{
    

    int[] invalide = new int[4];

    private int largeur;
    private int compteur = 0;
    private Graph graph;
    private Random gen = new Random();
    private List<Vector3> listePos = new List<Vector3>();
    private int P1, P2, P3, P4;
   
    
    int pasU = 0, pasD = 0, pasL = 0, pasR = 0;
    int[] restrictions = new int[4];
    private List<int> tousPointsVisités = new List<int>();
    public List<int> déjàVisités = new List<int>();
    public List<Vector3> ListePos
    {
        get => listePos;
    }
    public CréateurChemin2D(int Largeur, int p1, int p2, int p3, int p4)
    {
        largeur = Largeur;
        //graph = Graph;
        P1 = p1;
        P2 = p2;
        P3 = p3;
        P4 = p4;
        AssemblerChemin(p1, p2 , p3, p4);
        TransformerIntEnVector(tousPointsVisités);
        
    }
    private void AssemblerChemin(int p1, int p2, int p3, int p4)
    {
        /*DéterminerChemin2dAléatoire(0, 10);
        DéterminerChemin2dAléatoire(10, 28);
        DéterminerChemin2dAléatoire(28, 46);
        DéterminerChemin2dAléatoire(46, 32);
        DéterminerChemin2dAléatoire(32, 0);*/
        Debug.Log($"{p1} {p2} {p3} {p4}");
        DéterminerChemin2dAléatoire(0, p1);
        DéterminerChemin2dAléatoire(p1, p2);
        DéterminerChemin2dAléatoire(p2, p3);
        DéterminerChemin2dAléatoire(p3, p4);
        DéterminerChemin2dAléatoire(p4, 0);
        tousPointsVisités = RemoveDuplicates(tousPointsVisités);
        tousPointsVisités.Add(0);
       
    }
  

    public List<int> DéterminerChemin2dAléatoire(int pointDébut, int pointFin)
    {
        int newPoint = 0;
        int currentPos = pointDébut;
         compteur = 0;
        
        pasU = 0;
        pasD = 0;
        pasL = 0;
        pasR = 0;
        déjàVisités.Add(pointDébut);
        if (pointFin == 0)
        {
            tousPointsVisités.Remove(0);
        }
        CréerRestrictions(pointDébut, pointFin);
       
        do
        {
            do
            {
                newPoint = Bouger(currentPos, compteur);
                compteur++;
                if (VérifierSiRestePas() && newPoint != pointFin)
                {
                    
                    Debug.Log(newPoint);
                    déjàVisités.Clear();
                    List<int> reChemin = DéterminerChemin2dAléatoire(pointDébut, pointFin);
                
                    for (int i = 0; i < reChemin.Count - 1; i++)
                    {
                        déjàVisités.Add(reChemin[i]);
                    }
                    newPoint = pointFin;
                }
            } while (newPoint == -1 || (newPoint == 0 && pointFin != 0));
            Debug.Log(newPoint + " newPoint");
//            Debug.Log(graph.GetNeighbours(newPoint).Length + " length");
            //if (VérifierCasesAutour(newPoint, déjàVisités) == graph.GetNeighbours(newPoint).Length && newPoint !=  0)
            if (VérifierCasesAutour(newPoint, déjàVisités) == 4 && newPoint % 8 != 0 )
            {
                Debug.Log("MERDE");
                déjàVisités.Clear();
                Debug.Log(pointDébut + " MERDE");
                
                List<int> reChemin = DéterminerChemin2dAléatoire(pointDébut, pointFin);
                
                for (int i = 0; i < reChemin.Count - 1; i++)
                {
                    déjàVisités.Add(reChemin[i]);
                }
                newPoint = pointFin;
            }
           
            déjàVisités.Add(newPoint);
            currentPos = newPoint;
            
        } while (currentPos != pointFin);

        List<int> listeFinale = RemoveDuplicates(déjàVisités);
        listeFinale.Remove(pointFin);
        
        for (int i = 0; i < listeFinale.Count; i++)
        {
            tousPointsVisités.Add(listeFinale[i]);
        }
        déjàVisités.Clear();
        
        return  listeFinale;
        
    }
    
    
    // code de https://www.techiedelight.com/fr/remove-duplicates-from-list-csharp/
    private static List<T> RemoveDuplicates<T>(List<T> list) {
        return new HashSet<T>(list).ToList();
    }

    private bool VérifierSiRestePas() => (pasD >= restrictions[0] && pasL >= restrictions[2] &&
                                          pasR >= restrictions[3] && pasU >= restrictions[1]);

    private void CréerRestrictions(int pointDébut, int pointFin)
    {
        int pointF = pointFin;
        int distanceVert = 0;
        int distanceHor = 0;
        bool memeLigne = false;
        if ((float)pointDébut / largeur - pointDébut / largeur == 0)
        {
            pointF -= largeur;
        }
        while((pointF/largeur) != (pointDébut/largeur))
        {
            
            if (pointF  > pointDébut )
            {
                pointF -= largeur;
            }
            else
            {
                if (pointF  < pointDébut )
                {
                    pointF += largeur;
                }   
            }
            
        }
        
        distanceVert = pointF - pointDébut;

        if (distanceVert < 0)
        {
            restrictions[1] = -distanceVert;
            restrictions[0] = 0;
        }
        else
        {
            if (distanceVert > 0)
            {
                restrictions[0] = distanceVert;
                restrictions[1] = 0;
            }
            else
            {
                
                restrictions[0] = 1;
                restrictions[1] = 1;
            }
        }
        
        pointF = pointFin - distanceVert;
        
        for (int i = 0; distanceHor == 0 && !memeLigne; i++)
        {
            if (pointF < pointDébut)
            {
                if (pointF + i * largeur == pointDébut )
                {
                    distanceHor = -i;
                }
            }
            else
            {
                if (pointF > pointDébut)
                {
                    if (pointF - i * largeur == pointDébut)
                    {
                        distanceHor = i;
                    }
                }
                else
                {
                    memeLigne = true;
                }
            }
            
        }
        if (distanceHor < 0)
        {
            restrictions[2] = -distanceHor;
            restrictions[3] = 0;
        }
        else
        {
            if (distanceHor > 0)
            {
               restrictions[3] = distanceHor;
               restrictions[2] = 0; 
            }
            else
            {
                restrictions[3] = 1;
                restrictions[2] = 1;
            }
        }
    }
    private int Bouger(int pointDébut, int compteur)
    {
        int pos = pointDébut;
        int mouvement;
        int newPoint = 0;
        
        int compteInvalides = 0;

        for (int i = 0; i < invalide.Length; i++)
        {
            if (invalide[i] != 0)
            {
                compteInvalides++;
            }
        }

        
        mouvement = gen.Next(0, 4);
        if (compteInvalides == 3)
        {
            for (int i = 0; i < invalide.Length; i++)
            {
                if (invalide[i] == 0)
                {
                    mouvement = i;
                }

                invalide[i] = 0;
            }
        }
        if (compteur == 0 && pointDébut == 0)
        {
            mouvement = 0;
            
        }
        if ((pointDébut + 1) % 8 == 0 && compteur == 0)
        {
            mouvement = 3;
        }

        if (mouvement == 0)
        {
            
            if (pasD < restrictions[0])
            {
                newPoint = MoveDown(pos, déjàVisités);
                if (newPoint != -1)
                {
                    pasD++;
                }
            }
            else
            {
                newPoint = -1;
                invalide[0] = 1;
            }
        }
        else
        {
            if (mouvement == 1)
            {
               
                if (pasU < restrictions[1])
                {
                    newPoint = MoveUp(pos, déjàVisités);
                    if (newPoint != -1)
                    {
                        pasU++;
                    }
                }
                else
                {
                    newPoint = -1;
                    invalide[1] = 1;
                }

            }
            else
            {
                if (mouvement == 2)
                {
                    
                    if (pasL < restrictions[2])
                    {
                        newPoint = MoveLeft(pos, déjàVisités);
                        if (newPoint != -1)
                        {
                            pasL++;
                        }
                    }
                    else
                    {
                        newPoint = -1;
                        invalide[2] = 1;
                    }

                }

                if (mouvement == 3)
                {
                    if (pasR < restrictions[3])
                    {
                        newPoint = MoveRight(pos, déjàVisités);
                        if (newPoint != -1)
                        {
                            pasR++;
                        }
                    }
                    else
                    {
                        newPoint = -1;
                        invalide[3] = 1;
                    }

                }
            }

        }
            

        return newPoint;
    }

    private int VérifierEnHaut(int point, List<int> liste)
    {
        int compteurVerif = 0;
        
        if (point % largeur == 0)
        {
            compteurVerif++;
        }
        else
        {
            if (liste.Contains(point - 1) || tousPointsVisités.Contains(point -1))
            {
                compteurVerif++;
            }
        }

        return compteurVerif;
    }
    private int VérifierDroite(int point, List<int> liste)
    {
        int compteurVerif = 0;
        

        if (point + largeur > largeur * largeur)
        {
            compteurVerif++;
        }
        else
        {
            if (liste.Contains(point + largeur) || tousPointsVisités.Contains(point + largeur))
            {
                compteurVerif++;
            } 
        }

        return compteurVerif;
    }
    private int VérifierGauche(int point, List<int> liste)
    {
        int compteurVerif = 0;
        

        if (point - largeur > 0)
        {
            compteurVerif++;
        }
        else
        {
            if (liste.Contains(point - 1) || tousPointsVisités.Contains(point -1))
            {
                compteurVerif++;
            }
        }

        return compteurVerif;
    }
    private int VérifierEnBas(int point, List<int> liste)
    {
        int compteurVerif = 0;
        

        if (point + 1 % largeur == 0)
        {
            compteurVerif++;
        }
        else
        {
            if (liste.Contains(point + 1) || tousPointsVisités.Contains(point + 1))
            {
                compteurVerif++;
            }
        }

        return compteurVerif;
    }
    
    private int VérifierCasesAutour(int point, List<int> liste)
    {
        int compteurVerif = 0;
        compteurVerif += VérifierDroite(point, liste);
        compteurVerif += VérifierEnHaut(point, liste);
        compteurVerif += VérifierGauche(point, liste);
        compteurVerif += VérifierEnBas(point, liste);
        /*for (int i = 0; i < graph.GetNeighbours(point).Length; i++)
        {
            if (liste.Contains(graph.GetNeighbours(point)[i]) || tousPointsVisités.Contains(graph.GetNeighbours(point)[i]))
            {
                compteurVerif++;
            }
        }*/
        return compteurVerif;
    }
    
    
    private int MoveUp(int point, List<int> visités)
    {
        int nouvPoint = point - 1;
        if (point % largeur == 0 || visités.Contains(nouvPoint) || tousPointsVisités.Contains(nouvPoint))
            nouvPoint = -1;

        return nouvPoint;
    }
    private int MoveDown(int point, List<int> visités)
    {
        int nouvPoint = point + 1;
        if ((point + 1) %  largeur  == 0 || visités.Contains(nouvPoint) || tousPointsVisités.Contains(nouvPoint))
            nouvPoint = -1;
        
        return nouvPoint;
    }
    private int MoveLeft(int point, List<int> visités)
    {
        int nouvPoint = point - largeur;
        if (nouvPoint < 0 || visités.Contains(nouvPoint) || tousPointsVisités.Contains(nouvPoint))
            nouvPoint = -1;
       
        return nouvPoint;
    }
    private int MoveRight(int point, List<int> visités)
    {
        int nouvPoint = point + largeur;
        if (nouvPoint > largeur * largeur - 1 || visités.Contains(nouvPoint) || tousPointsVisités.Contains(nouvPoint))
            nouvPoint = -1;
       
        return nouvPoint;
    }

    private void TransformerIntEnVector(List<int> list)
    {
        List<Vector3> positions = new List<Vector3>(largeur * largeur);
        for (int i = 0; i < largeur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                positions.Add(new Vector3(i * 150, 0, j * 150)); 
            }
        }
        listePos.Add(new Vector3(0,0,0));
        for (int i = 0; i < list.Count; i++)
        {
            listePos.Add(positions[list[i]]);
        }

        
    }
    
    
    
}
