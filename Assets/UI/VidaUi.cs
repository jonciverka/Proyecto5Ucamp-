using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaUi : MonoBehaviour
{
    [SerializeField]
    private Image derechaVida;
    [SerializeField]
    private Image izquierdaVida;
    private GameObject playerGo;
    private Player player;
    void Start()
    {
        playerGo = GameObject.FindGameObjectsWithTag("Player")[0];
        player = playerGo.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        izquierdaVida.fillAmount = player.getVida() / 100;
        derechaVida.fillAmount = player.getVida() / 100;
        if(izquierdaVida.fillAmount>.60){
            izquierdaVida.color = new Color(255, 255, 255);
            derechaVida.color = new Color(255, 255, 255);
        }else if(izquierdaVida.fillAmount<=.60 && izquierdaVida.fillAmount>=.30){
            izquierdaVida.color = new Color(1.000f, 0.729f, 0.729f, 1.000f);
            derechaVida.color = new Color(1.000f, 0.729f, 0.729f, 1.000f);
        }else if(izquierdaVida.fillAmount<.30){
            izquierdaVida.color = new Color(255, 0, 0);
            derechaVida.color = new Color(255, 0, 0);
        }
    }
    
}
