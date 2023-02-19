using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// source.https://www.youtube.com/watch?v=p-3S73MaDP8&t=556s&ab_channel=Brackeys

public class GestionnaireTouches : BehaviourAuto
{
    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Avancer.performed += ctx => Avancer(); // ctx = contexte

        controls.Gameplay.Arrêter.performed += ctx => Arrêter();

        controls.Gameplay.Accélérer.performed += ctx => Accélérer();
            
        controls.Gameplay.TournerGauche.performed += ctx => TournerGauche();

        controls.Gameplay.TournerDroite.performed += ctx => TournerDroite();

        controls.Gameplay.Bonus.performed += ctx => GestionBonus();
        // faire une fonction qui va gérer les bonus dans gestion du player
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
