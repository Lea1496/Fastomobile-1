using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum?
public interface Chassis
{
    enum Niveau
    {
        Léger,
        Normal,
        Lourd
    }
    public int Poids {get; set;}
}
