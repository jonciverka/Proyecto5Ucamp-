using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField]
    private GameObject arriba;
    [SerializeField]
    private GameObject abajo;
    [SerializeField]
    private GameObject derecha;
    [SerializeField]
    private GameObject izquierda;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private bool isApuntando = false;
    [SerializeField]
    private CanvasGroup hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void  apuntar(){
        if(isApuntando == false){
            isApuntando = true;
            arriba.transform.position = new Vector3 (arriba.transform.position.x,arriba.transform.position.y-16,arriba.transform.position.z);
            abajo.transform.position = new Vector3 (abajo.transform.position.x,abajo.transform.position.y+16,abajo.transform.position.z);
            derecha.transform.position = new Vector3 (derecha.transform.position.x+16,derecha.transform.position.y,derecha.transform.position.z);
            izquierda.transform.position = new Vector3 (izquierda.transform.position.x-16,izquierda.transform.position.y,izquierda.transform.position.z);
        }
    }
    public void  normal(){
       if(isApuntando == true){
            isApuntando = false;
            arriba.transform.position = new Vector3 (arriba.transform.position.x,arriba.transform.position.y+16,arriba.transform.position.z);
            abajo.transform.position = new Vector3 (abajo.transform.position.x,abajo.transform.position.y-16,abajo.transform.position.z);
            derecha.transform.position = new Vector3 (derecha.transform.position.x-16,derecha.transform.position.y,derecha.transform.position.z);
            izquierda.transform.position = new Vector3 (izquierda.transform.position.x+16,izquierda.transform.position.y,izquierda.transform.position.z);
        }
    }
    public void esconderUi()
    {
        if(canvasGroup.alpha==1){
            StartCoroutine(FadeImageHit(1f, 0f));
        }
    }

    IEnumerator FadeImageHit(float startAlpha, float targetAlpha)
    {
        float currentTime = 0f;

        while (currentTime < 1f)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / .5f);
            canvasGroup.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
   public void mostrarUi(){
        StopCoroutine(FadeImageHit(1f, 0f));
        if(canvasGroup.alpha!=1){
            StartCoroutine(FadeImageHit(0f, 1f));
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
     public void esconderHit(){
        print("esconderHit");
        hit.alpha = 0;
    }

    IEnumerator FadeHit(float startAlpha, float targetAlpha)
    {
        float currentTime = 0f;

        while (currentTime <= 1f)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / .05f);
            hit.alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
        hit.alpha = targetAlpha;
        esconderHit();

    }
   public void mostrarHit(){
        if(hit.alpha!=1){
            StartCoroutine(FadeHit(0f, 1f));
        }
    }
}
