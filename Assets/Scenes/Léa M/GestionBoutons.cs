using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionBoutons : MonoBehaviour
{
    public void DémarrerJeu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChoixVéhicule()
    {
        
    }
    
}
