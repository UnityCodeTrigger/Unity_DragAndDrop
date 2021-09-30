using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 mousePos;

    private void OnMouseDown()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

        //guarda la posicion inicial.
        startPosition = transform.position;

    }

    private void OnMouseDrag()
    {

        //El Objeto persiga al raton en la posicion de la escena y no la pantalla

        //Mover el objeto en pantalla hacia el raton.
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y,transform.position.z);

    }

    private void OnMouseUp()
    {

        gameObject.GetComponent<Collider2D>().enabled = true;

        //Detecta colliders
        DetectColliders();
        //DetectLimitScreen
        ScreenLimit();

    }

    private void DetectColliders()
    {

        //Crear boxcastAll para detectar si hay un collider superpuesto a otro.
        RaycastHit2D[] hit;

        hit = Physics2D.BoxCastAll(transform.position, transform.localScale, transform.rotation.z, transform.rotation.eulerAngles);

        //Detectar otro objeto con el tag "Player"
        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("He impactado con: " + hit[i].transform.gameObject.name);

            //El objeto impactado es este objeto?
            //el objeto impactado es un objeto con la etiqueta "Player"?
            if(hit[i].transform.gameObject != this.gameObject && hit[i].transform.CompareTag("Player")) 
            {

                //vuelve a la posicion inicial.
                transform.position = startPosition;

            }

        }

    }

    private void ScreenLimit()
    {

        Vector3 screenSize = new Vector3(Screen.width, Screen.height, 0);
        screenSize = Camera.main.ScreenToWorldPoint(screenSize);

        Vector3 transPos = transform.position;

        //Posicion X
        if (transPos.x >screenSize.x)
        {

            transform.position = startPosition;

        }
        if (transPos.x < -screenSize.x)
        {

            transform.position = startPosition;

        }

        //Posicion Y
        if (transPos.y > screenSize.y)
        {

            transform.position = startPosition;

        }
        if (transPos.y < -screenSize.y)
        {

            transform.position = startPosition;

        }

    }

}
