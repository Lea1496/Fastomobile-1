using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Code utilisé en classe en session 3
/// </summary>




//le graphe ne peut pas changer en terme de nombre de noeuds après sa construction
//par contre, nous pourrons ajouter des connexions entre noeuds après la construction
public class Graph
{
    public readonly int nNodes;
    private SortedSet<int>[] adjacencyLists;
    // le sortedSet évite les doublons jsp trop et c'est un set trié
    public Graph(int nNodes)
    {
        this.nNodes = nNodes;
        adjacencyLists = new SortedSet<int>[nNodes];
        for (int i = 0; i < nNodes; ++i)
        {
            adjacencyLists[i] = new SortedSet<int>();
        }
    }
    
    //bidirectionel

    public void AddDirectEdge(int nodeA, int nodeB)
    {
        adjacencyLists[nodeA].Add(nodeB);
        adjacencyLists[nodeB].Add(nodeA);
    }
    //ilfaut retourner une copie des voisins
    public int[] GetNeighbours(int node) => adjacencyLists[node].ToArray();

    public string CreateAdjacencyListsDisplay()
    {
        StringBuilder sb = new StringBuilder(nNodes * 10); // ici c'est parce qu'on aurait droit a 10 caractères par noeuds

        //pour chaque noeud...
        for (int i = 0; i < nNodes; i++)
        {
            sb.Append($"Node {i} : ");
            int[] currentNodeNeighbours = GetNeighbours(i);
            
            //pour chaque voisin du noeud courant
            for (int j = 0; j < currentNodeNeighbours.Length; j++)
            {
                sb.Append($"{currentNodeNeighbours[j]} ");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}