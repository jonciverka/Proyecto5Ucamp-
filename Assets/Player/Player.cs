using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    public float speed = 3f;
    public float mouseSensitivity = 2f;
    [SerializeField]
    private Camera cam;
    private float rotationX = 0f;
    private float rotationY = 0f;
    [SerializeField]
    private GameObject positionCamera;
    [SerializeField]
    private GameObject positionCameraCorriendo;
    [SerializeField]
    private GameObject PivoteCamaraAgachado;
    private AudioSource audioSourceDisparo;
    [SerializeField]
    private GameObject puntoDisparoDerecha;
    [SerializeField]
    private GameObject puntoDisparoIzquierda;
    [SerializeField]
    private GameObject  efectoDisparo;
    [SerializeField]
    private GameObject  choqueDisparo;
    private GameObject gameController;
    private EstaminaUI estaminaUI;
    private DanioUi danioUi;
    private Aim aim;
    [SerializeField]
    private EnergiaPistola energiaPistolaDerecha;
    [SerializeField]
    private EnergiaPistola energiaPistolaIzquieda;

    public float vida = 100f;
    public int poder = 2;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        audioSourceDisparo = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
        estaminaUI = gameController.GetComponent<EstaminaUI>();
        danioUi = gameController.GetComponent<DanioUi>();
        aim = gameController.GetComponent<Aim>();
    }

    // Update is called once per frame
    void Update(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(animator.GetBool("Corriendo")){
            horizontal = 0f;
        }

        Vector3 cameraForward = transform.forward;
        Vector3 cameraRight = transform.right;
        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal) * speed;
        if(animator.GetBool("Hincarse")==false){
            characterController.Move(new Vector3(movement.x,0f, movement.z) * Time.deltaTime);
        }
        
         if (movement != Vector3.zero){
            animator.SetBool("arrodillarse", false);
            animator.SetBool("Caminando", true);
        }else{
            animator.SetBool("Caminando", false);

        }
        if(animator.GetBool("Corriendo")==false){
            rotarCamara();
        }
        disparar();
        correr();
        hincarse();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string currentStateName = stateInfo.shortNameHash.ToString();
        if(stateInfo.IsName("IdleNormal")==true ||  stateInfo.IsName("Caminar")==true || stateInfo.IsName("Corriendo")==true){
            energiaPistolaDerecha.mostrarUi();
            energiaPistolaIzquieda.mostrarUi();
        }else{
            energiaPistolaIzquieda.esconderUi();
            energiaPistolaDerecha.esconderUi();
        }
    }
    void hincarse(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(animator.GetBool("arrodillarse")==false){
                animator.SetBool("arrodillarse", true);
            }else{
                animator.SetBool("arrodillarse", false);
            }
        }
       


    }
    void rotarCamara(){
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); 
        transform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0f);
    }
    void disparar(){
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1)){
             animator.SetBool("arrodillarse", false);
            if(energiaPistolaIzquieda.sinEnerfiaFc()==false){
                aim.apuntar();
                audioSourceDisparo.Play();
                animator.SetTrigger("DispararIzquierda");
                energiaPistolaIzquieda.sumarEnergia();
                var disparoEfectoIzquierda = Instantiate(efectoDisparo, puntoDisparoIzquierda.transform.position, Quaternion.Euler(-90, 0, 0));
                Destroy(disparoEfectoIzquierda, 1f);
                if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(new Vector3(0,0.25f,1)), out hit, Mathf.Infinity, layerMask)){
                    var golpeDisparar = Instantiate(choqueDisparo,hit.point, Quaternion.identity);
                    Destroy(golpeDisparar, 1f);
                    if(hit.collider.gameObject.tag=="Enemigo"){
                        aim.mostrarHit();
                        hit.collider.gameObject.GetComponent<Torreta>().danio(30);
                    }
                    if(hit.collider.gameObject.tag=="EnemigoPersona"){
                        aim.mostrarHit();
                        hit.collider.gameObject.GetComponent<Enemigo>().danio(30);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0)){
             animator.SetBool("arrodillarse", false);
            if(energiaPistolaDerecha.sinEnerfiaFc()==false){
                aim.apuntar();
                audioSourceDisparo.Play();
                animator.SetTrigger("DispararDerecha");
                energiaPistolaDerecha.sumarEnergia();
                var disparoEfectoDerecha = Instantiate(efectoDisparo, puntoDisparoDerecha.transform.position, Quaternion.Euler(-90, 0, 0));
                Destroy(disparoEfectoDerecha, 1f);
                if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(new Vector3(0,0.25f,1)), out hit, Mathf.Infinity, layerMask)){
                    var golpeDisparar = Instantiate(choqueDisparo,hit.point, Quaternion.identity);
                    Destroy(golpeDisparar, 1f);
                    if(hit.collider.gameObject.tag=="Enemigo"){
                        aim.mostrarHit();
                        hit.collider.gameObject.GetComponent<Torreta>().danio(30);
                    }
                    if(hit.collider.gameObject.tag=="EnemigoPersona"){
                        aim.mostrarHit();
                        hit.collider.gameObject.GetComponent<Enemigo>().danio(30);
                    }
                }
            }

        }

    }
    void correr(){
        
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            aim.normal();
            estaminaUI.mostrarUi();
            aim.esconderUi();
            animator.SetBool("arrodillarse", false);
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            if(estaminaUI.getEsCansado() == false){
                estaminaUI.utilizandoStaminaFn(true);
                animator.SetBool("Corriendo", true);
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, positionCameraCorriendo.transform.position, Time.deltaTime * 2f );
                speed = 8f;
            }else{
                 estaminaUI.utilizandoStaminaFn(false);
                animator.SetBool("Corriendo", false);
                aim.mostrarUi();
                speed = 3f;
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, positionCamera.transform.position, Time.deltaTime * 3f );
            }
        }else if(animator.GetBool("arrodillarse")==false && animator.GetBool("arrodillarse")==false){
            estaminaUI.utilizandoStaminaFn(false);
            animator.SetBool("Corriendo", false);
            aim.mostrarUi();
            speed = 3f;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, positionCamera.transform.position, Time.deltaTime * 3f );
        }

    }


    public void danio(int danio){
        danioUi.mostrarUi();
        vida = vida - danio;
        if(vida<=0){
            danioUi.mostrarGameOver();
        }
    }
    public float getVida(){
        return vida;
    }
}

