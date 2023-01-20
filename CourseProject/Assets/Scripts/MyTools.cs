using System.Collections;
using System.Collections.Generic;
using Kryz.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;

public delegate float EasingFuncDelegate(float t);

public static class MyTools 
{
    public static void LOG(Component component,string msg)
    {
        Debug.Log(Time.frameCount + " - " + component.gameObject.name+" - "
                  +component.GetType()+" - "+  msg);
    }
 
    public static bool ColorizeRandom(GameObject gameObject)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        if (mr)
            mr.material.color = Random.ColorHSV();
 
        return mr != null;
    }
 
    public static IEnumerator TranslationCoroutine(Transform transform,Vector3 startPos, Vector3 endPos, float translationSpeed, 
        EasingFuncDelegate easingFunc = null)
    {
        if (translationSpeed <= 0) yield break;
 
        float elapsedTime = 0;
        float duration = Vector3.Distance(startPos, endPos) / translationSpeed;
 
        while (elapsedTime < duration)
        {
            float k = elapsedTime / duration;
 
            transform.position = Vector3.Lerp(startPos, endPos, easingFunc != null? easingFunc(k):k);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }
    
    public static IEnumerator BallisticsMvtCoroutine(Transform transform, Vector3 startPos, Vector3 endPos, float duration, 
        EasingFuncDelegate easingFunc = null, Action startAction = null, Action endAction = null)
    {
        if (duration <= 0) yield break;
        
        if(startAction != null) startAction();
        
        float elapsedTime = 0;
        Vector3 startVel = (endPos - startPos - Physics.gravity * duration * duration) / duration; // vitesse initial
        while (elapsedTime < duration)
        {
            float k = elapsedTime / duration;
            float t = elapsedTime * (easingFunc != null ? easingFunc(k):k);
 
            transform.position = Physics.gravity * t * t + startVel * t + startPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        
        if(endAction != null) endAction();
    }
}