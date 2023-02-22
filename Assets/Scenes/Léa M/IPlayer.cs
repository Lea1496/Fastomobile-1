using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayer
{
  int Vie { get; }
  string Nom { get;}
  int Argent { get; }
  int Puissance { get; }
  int IdVéhicule { get; }
  int IdMoteur { get; }
  
}
