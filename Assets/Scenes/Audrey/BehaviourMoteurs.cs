using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum?
public interface Moteurs
{
    enum Type
    {
        Débutant, // je me rappelle plus c'est quoi le premier
        Avancé,
        Pro
    }
    public int Puissance { get; set; }
}
