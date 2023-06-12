using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject puntoDisparoDerecha;
    [SerializeField]
    private AudioSource audioSourceDisparo;
    [SerializeField]
    private GameObject puntoDisparoIzquierda;
    [SerializeField]
    private GameObject  efectoDisparo;
    [SerializeField]
    private GameObject  choqueDisparo;    
    public Transform objetoASeguir;
    public float aux = 0f;
    [SerializeField]
    private Image vidaUi;
    public float vida = 100f;
    [SerializeField]
    private GameObject  humo;
    private bool conVida = true;
     IEnumerator Start()
    {

        animator = GetComponent<Animator>();
        while(conVida)
        {
            inicio();
            yield return new WaitForSeconds(2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(conVida){
            perseguir();
            disparar();
           
        }

    }
    private void   inicio(){
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("IdleApuntando")==true || stateInfo.IsName("IncarseIncado")==true){
            int randomNumber = Random.Range(0, 10);
            if (randomNumber % 2 == 0){
            }else{
                animator.SetTrigger("Disparar");
            }
        }
    }
     void disparar(){
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("DispararDerecha")==true){
            audioSourceDisparo.Play();
            var disparoEfectoIzquierda = Instantiate(efectoDisparo, puntoDisparoDerecha.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(disparoEfectoIzquierda, 1f);
            if (Physics.Raycast(puntoDisparoDerecha.transform.position, puntoDisparoDerecha.transform.TransformDirection(new Vector3(0,aux,1)), out hit, Mathf.Infinity, layerMask)){
                var golpeDisparar = Instantiate(choqueDisparo,hit.point, Quaternion.identity);
                Destroy(golpeDisparar, 1f);
                if(hit.collider.gameObject.tag=="Player"){
                    hit.collider.gameObject.GetComponent<Player>().danio(1);
                }
            }
        }
        if (stateInfo.IsName("DispararDerecha")==true){
            audioSourceDisparo.Play();
            var disparoEfectoDerecha = Instantiate(efectoDisparo, puntoDisparoIzquierda.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(disparoEfectoDerecha, 1f);
             if (Physics.Raycast(puntoDisparoIzquierda.transform.position, puntoDisparoDerecha.transform.TransformDirection(new Vector3(0,aux,1)), out hit, Mathf.Infinity, layerMask)){
                var golpeDisparar = Instantiate(choqueDisparo,hit.point, Quaternion.identity);
                Destroy(golpeDisparar, 1f);
                if(hit.collider.gameObject.tag=="Player"){
                    hit.collider.gameObject.GetComponent<Player>().danio(1);
                }
            }

        }
    }
    private void perseguir(){
        Vector3 baseTargetPosition = new Vector3(objetoASeguir.position.x, transform.position.y, objetoASeguir.position.z);
        Quaternion baseTargetRotation = Quaternion.LookRotation(baseTargetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, baseTargetRotation, 10f * Time.deltaTime);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Debug.DrawRay(puntoDisparoDerecha.transform.position, puntoDisparoDerecha.transform.TransformDirection(new Vector3(0,aux,1)) * 10000);
        Debug.DrawRay(puntoDisparoIzquierda.transform.position, puntoDisparoDerecha.transform.TransformDirection(new Vector3(0,aux,1)) * 10000);
    }
    void vidaFnc(){
        vidaUi.fillAmount = vida / 100;
        if(vidaUi.fillAmount>.60){
            vidaUi.color = new Color(255, 255, 255);
        }else if(vidaUi.fillAmount<=.60 && vidaUi.fillAmount>=.30){
            vidaUi.color = new Color(1.000f, 0.729f, 0.729f, 1.000f);
        }else if(vidaUi.fillAmount<.30){
            vidaUi.color = new Color(255, 0, 0);
        }
    }
     public void danio(int danio){
        vida = vida - danio;
        vidaFnc();
        if(vida<=0){
            conVida = false;
            Instantiate(humo, transform.position, Quaternion.Euler(-90, 0, 0));
        }
    }
}
