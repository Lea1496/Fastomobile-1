
using UnityEngine;

public class CréateurTerrain
{
    

    private float demiLongueur = 150f;
    
    
    private Vector3 position = new Vector3(0, -0.5f, 0);
    public CréateurTerrain(int largeur, GameObject terrain)
    {
        for (int i = 0; i < largeur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                GameObject.Instantiate(terrain, position, terrain.transform.rotation);
                position.x += demiLongueur;
            }

            position.x = 0;
            position.z += demiLongueur;
        }
    }

   
}
