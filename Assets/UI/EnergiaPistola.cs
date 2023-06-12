using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergiaPistola : MonoBehaviour
{
    [SerializeField]
    public Image energiaUI;
    public Image energiaUIFuera;
    public Image energiaUIFueraIcono;
    private float energia = 0f;
    private bool sinEnergia = false;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private bool aux = true;
     IEnumerator  Start()
    {
        energiaUI.fillAmount = energia;
        energiaUIFuera.fillAmount = energia;
        while (true){
            restarEnergia();
            yield return new WaitForSeconds(.2f);
        }
    }

    public void restarEnergia(){
        if(energia>0){
            energia = energia - .01f;
            energiaUI.fillAmount = energia;
            energiaUIFuera.fillAmount = energia;
        }else if(energia<=0){
            energiaUIFueraIcono.color = new Color(255, 255, 255);
            sinEnergia = false;
        }
    }
    public void sumarEnergia(){
        if(energia<1){
            energia = energia + .1f;
            energiaUI.fillAmount = energia;
            energiaUIFuera.fillAmount = energia;
        }else if(energia>=1){
            energiaUIFueraIcono.color =new Color(255, 0, 0);
            sinEnergia = true;
        }
    }
    public bool sinEnerfiaFc(){
        return sinEnergia;
    }
    public void esconderUi()
    {
        if(canvasGroup.alpha==1){
            aux = true;
            canvasGroup.alpha = 0;
            StartCoroutine(FadeImage(1f, 0f));
        }
    }
    IEnumerator FadeImage(float startAlpha, float targetAlpha)
    {
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
        if(canvasGroup.alpha!=1 && aux){
            aux = false;
            canvasGroup.alpha = 1;
            StartCoroutine(FadeImage(0f, 1f));
        }
    }
}

