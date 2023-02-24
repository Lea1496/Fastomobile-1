using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PasserLigneBehavior : MonoBehaviour
{

    [SerializeField] private UnityEvent<int>[] spawners;
    
    
    private int triggerCount = 0;
    private int compteurTour = 0;

    public int CompteurTour
    {
        get => compteurTour;
    }
    private void OnTriggerEnter(Collider collider)
    {
        compteurTour++;
    }

    
    
}
