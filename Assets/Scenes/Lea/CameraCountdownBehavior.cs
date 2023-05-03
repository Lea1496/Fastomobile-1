using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class CameraCountdownBehavior : MonoBehaviour
{
    //Code de :
    //https://www.youtube.com/watch?v=ulxXGht5D2U
    public int countDownTime;
    public Text texte;

    private void Start()
    {
        StartCoroutine(CountDownToStart());
    }

    private IEnumerator CountDownToStart()
    {
        while (countDownTime > 0)
        {
            texte.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
        
        texte.text = "GO!";
        yield return new WaitForSeconds(1f);
        texte.gameObject.SetActive(false);
    }
}
