
using UnityEngine;
/// <summary>
/// interface représentant le joueur 
/// </summary>
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
  
  bool IsMainPlayer { get; }
  bool IsMainPlayer1 { get; }
  bool IsMainPlayer2 { get; }
  
  int Tour { get; }
  bool IsFinished { get; }
  
}
