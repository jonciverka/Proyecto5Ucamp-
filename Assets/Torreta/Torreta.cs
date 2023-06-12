using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torreta : MonoBehaviour
{
    public Transform objetoASeguir;
    public Transform baseTorreta;
    public Transform canonTorreta;
    private AudioSource audioSourceDisparo;
    public Transform Hueco;
    [SerializeField]
    private GameObject  efectoDisparo;
    [SerializeField]
    private GameObject  choqueDisparo;
    private bool dentroDelTrigger = false;
    private Coroutine b;
    [SerializeField] private float radioDeteccion;
    private bool dentroDelRadio = false;
    public float vida = 100f;
    [SerializeField]
    private Image vidaUi;
    [SerializeField]
    private Transform SeguiminetoMuerto;
     [SerializeField]
    private GameObject  humo;

    private void  Start()
    {
        audioSourceDisparo = GetComponent<AudioSource>();
        
    }
    private void Update() {
        perseguir();
        if(vida>=100){
            dectectPlayer();
        }
        vidaFnc();
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
    void disparar(){
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        audioSourceDisparo.Play();
        var disparo = Instantiate(efectoDisparo, Hueco.transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(disparo, 1f);
        if (Physics.Raycast(Hueco.transform.position, Hueco.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)){
            var golpeDisparar = Instantiate(choqueDisparo,hit.point, Quaternion.identity);
            Destroy(golpeDisparar, 1f);
            if(hit.collider.gameObject.tag=="Player"){
                hit.collider.gameObject.GetComponent<Player>().danio(10);
            }
        }
    }
    private void dectectPlayer(){
         bool encontrados = false;
        Collider[] objetos = Physics.OverlapSphere(transform.position, radioDeteccion);
        foreach (Collider collider in objetos)
        {
            if (collider.CompareTag("Player"))
            {
                encontrados = true;
                break;
            }
        }

         if (encontrados && !dentroDelRadio)
        {
            dentroDelRadio = true;
            StartCoroutine(DispararEsperar());
        }
        else if (!encontrados && dentroDelRadio)
        {
            dentroDelRadio = false;
        }
        
    }
    private void perseguir(){
        Vector3 baseTargetPosition = new Vector3(objetoASeguir.position.x, baseTorreta.position.y, objetoASeguir.position.z);
        Quaternion baseTargetRotation = Quaternion.LookRotation(baseTargetPosition - baseTorreta.position);
        baseTorreta.rotation = Quaternion.Lerp(baseTorreta.rotation, baseTargetRotation, 10f * Time.deltaTime);
        Quaternion canonTargetRotation = Quaternion.LookRotation(objetoASeguir.position  - canonTorreta.position);
        canonTorreta.rotation = Quaternion.Lerp(canonTorreta.rotation, canonTargetRotation, 10f * Time.deltaTime);
    }
    private void OnDrawGizmos() {
        Debug.DrawRay(baseTorreta.transform.position, baseTorreta.transform.TransformDirection(Vector3.forward), Color.yellow );
        Debug.DrawRay(canonTorreta.transform.position, canonTorreta.transform.TransformDirection(Vector3.forward), Color.red );
        Debug.DrawRay(Hueco.transform.position, Hueco.transform.TransformDirection(Vector3.forward), Color.white );
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }    
    IEnumerator DispararEsperar(){
        while (dentroDelRadio){
            if(vida>=100){
                disparar();
            }   
            yield return new WaitForSeconds(1f);
        }
    }
    public void danio(int danio){
        vida = vida - danio;
        if(vida<=0){
            objetoASeguir = SeguiminetoMuerto;
            Instantiate(humo, transform.position, Quaternion.Euler(-90, 0, 0));


        }
    }
}
