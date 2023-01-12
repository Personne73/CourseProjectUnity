using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTools
{
    public static bool ColorizeRandom(GameObject gameObject)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        if(mr) mr.material.color = Random.ColorHSV();
        return mr != null;
    }

    public static void LOG(Component component, string msg)
    {
        Debug.Log(Time.frameCount + " - " + component.gameObject.name 
                  + " - " + component.GetType() + " - " + msg);
    }
}
