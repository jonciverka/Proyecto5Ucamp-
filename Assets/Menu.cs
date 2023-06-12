using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
     Vector3 object_pos;
     float angle;
    void Update()
    {
     mouse_pos = Input.mousePosition;
     mouse_pos.z = 5.23f; //The distance between the camera and object
     object_pos = Camera.main.WorldToScreenPoint(target.position);
     mouse_pos.x = mouse_pos.x - object_pos.x;
     mouse_pos.y = mouse_pos.y - object_pos.y;
     angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
     target.rotation = Quaternion.Euler(new Vector3(0, angle + 90, 0));
    }
    public void iniciarJuego(){
         SceneManager.LoadScene("Nivel1");

    }
}
