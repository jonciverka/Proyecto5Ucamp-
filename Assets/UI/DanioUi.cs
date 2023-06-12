using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanioUi : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private CanvasGroup canvasGroupGameOver;

    IEnumerator esconderUi(){
         yield return new WaitForSeconds(2);
        if(canvasGroup.alpha==1){
            canvasGroup.alpha = 0;
            StartCoroutine(FadeDanio(1f, 0f));
        }
    }
    IEnumerator FadeDanio(float startAlpha, float targetAlpha){
        float currentTime = 0f;
        while (currentTime < 1f)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / 1f);
            canvasGroup.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void mostrarUi(){
        canvasGroup.alpha = 1;
        StartCoroutine(esconderUi());
    }
     public void mostrarGameOver(){
        canvasGroupGameOver.alpha = 1;
    }



}
