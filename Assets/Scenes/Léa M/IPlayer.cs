using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayer
{
  int Vie { get; }
  string Nom { get;}
  int Argent { get; }
  int IdVéhicule { get; }
  int IdMoteur { get; }
  int Puissance { get; }
  GameObject Chassis { get; }
  int Poids { get; }

  
}
