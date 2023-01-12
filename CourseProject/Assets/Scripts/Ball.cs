using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask m_ColorizableLayerMask;
    private void OnCollisionEnter(Collision collision)
    {
        // IDENTIFICATION PAR NOM - pas ouf, c'est faible -
        // if (collision.gameObject.name.Equals("Cube"))
        //if(collision.gameObject.name.ToUpper().Contains("CUBE")) MyTools.ColorizeRandom(collision.gameObject);
        
        // IDENTIFICATION PAR TAG - pas ouf, c'est faible -
        //if(collision.gameObject.CompareTag("Colorizable")) MyTools.ColorizeRandom(collision.gameObject);
        
        // IDENTIFICATION PAR LAYER 
        // if((m_ColorizableLayerMask.value & (1 << collision.gameObject.layer)) != 0) 
        //     MyTools.ColorizeRandom(collision.gameObject);
        
        // IDENTIFICATION PAR COMPONENT TAG
        // if (null != collision.gameObject.GetComponent<ColorizableTag>())
        //     MyTools.ColorizeRandom(collision.gameObject);
        
        // IDENTIFICATION FONCTIONNELLE PAR INTERFACE
        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();
        if (null != destroyable)
            destroyable.Kill();
    }
}
