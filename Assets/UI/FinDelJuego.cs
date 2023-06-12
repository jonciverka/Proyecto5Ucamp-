using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDelJuego : MonoBehaviour
{
    // Start is called before the first frame update
    private bool restante = true;
    [SerializeField]
    private CanvasGroup canvasGanar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        restante = true;
        GameObject[] enemigo = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject ene in enemigo)
        {
            print(ene.GetComponent<Torreta>().vida);
            if(ene.GetComponent<Torreta>().vida>0){
                restante = false;
            }
        }
        if(restante){
            canvasGanar.alpha = 1;
        }
        

    }
}
