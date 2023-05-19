using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AfficherManettes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manette1;

    [SerializeField] private TextMeshProUGUI manette2;
    
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            manette1.text = $"Joueur1 : {Gamepad.all[0].displayName}";
            if (Gamepad.all.Count > 1)
            {
                manette2.text = $"Joueur2 : {Gamepad.all[1].displayName}";
            }
        }
    }
}
