using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EstaminaUI : MonoBehaviour
{
    [SerializeField]
    public Image staminaUI;
    private float stamina = 1f;
    private bool utilizandoStamina = false;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private bool esCansado = false;
    IEnumerator  Start()
    {
        staminaUI.fillAmount = stamina;
        while (true){
            if(utilizandoStamina){
                restarStamina();
            }else{
                sumarStamina();
            }
            yield return new WaitForSeconds(.03f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(staminaUI.fillAmount>.60){
            staminaUI.color = new Color(255, 255, 255);
        }else if(staminaUI.fillAmount<=.60 && staminaUI.fillAmount>=.30){
            staminaUI.color = new Color(1.000f, 0.729f, 0.729f, 1.000f);
        }else if(staminaUI.fillAmount<.30){
            staminaUI.color = new Color(255, 0, 0);
        }
    }
    public void restarStamina(){
        if(stamina>0){
            stamina = stamina - .005f;
            staminaUI.fillAmount = stamina;
        }else if(stamina<=0){
            esCansado = true;
        }
    }
    public void sumarStamina(){
        if(stamina<1){
            stamina = stamina + .01f;
            staminaUI.fillAmount = stamina;
        }else if(stamina>=1){
            esCansado = false;
            esconderUi();
        }
    }
    
    public void utilizandoStaminaFn(bool estado){
        utilizandoStamina = estado;
    }

    public bool getEsCansado(){
        return esCansado;
    }


    public void esconderUi()
    {
        if(canvasGroup.alpha==1){
            StartCoroutine(FadeImageEstamina(1f, 0f));
        }
    }

    IEnumerator FadeImageEstamina(float startAlpha, float targetAlpha)
    {
        float currentTime = 0f;

        while (currentTime < 1f) {
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
        StopCoroutine(FadeImageEstamina(1f, 0f));
        if(canvasGroup.alpha!=1){
            StartCoroutine(FadeImageEstamina(0f, 1f));
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
