using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextesData : MonoBehaviour
{
    [SerializeField] private Text mauvaisSens;
    [SerializeField] private Text mauvaisSens2;
    [SerializeField] private Text mauvaisSens3;

    public Text[] lesTextes;

    private void Start()
    {
        lesTextes = new Text[] { mauvaisSens, mauvaisSens2, mauvaisSens3 };
    }
}
