using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraCountdownBehavior : MonoBehaviour
{
    //Code de :
    //https://www.google.com/search?q=how+to+show+a+countdown+on+camera+unity&sxsrf=AJOqlzXj_DnKO7SWDVuE5j3_e_yKjQJO3g%3A1677253186246&ei=Qtr4Y9naDq_dkPIPq_eOgAw&ved=0ahUKEwjZg_WCv679AhWvLkQIHau7A8AQ4dUDCA8&uact=5&oq=how+to+show+a+countdown+on+camera+unity&gs_lcp=Cgxnd3Mtd2l6LXNlcnAQAzIFCCEQoAE6CggAEEcQ1gQQsAM6BAgjECc6BAghEBU6CwghEBYQHhDxBBAdOgcIIRCgARAKSgQIQRgAUNQDWK0SYLsUaAJwAXgAgAGRAYgBjgeSAQMwLjeYAQCgAQHIAQjAAQE&sclient=gws-wiz-serp#kpvalbx=_R9r4Y7fxMO7PkPIP64C8oAM_40
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
