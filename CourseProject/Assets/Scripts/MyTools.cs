using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
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
 
    public static IEnumerator TranslationCoroutine(Transform transform,Vector3 startPos, Vector3 endPos, float translationSpeed)
    {
        if (translationSpeed <= 0) yield break;
 
        float elapsedTime = 0;
        float duration = Vector3.Distance(startPos, endPos) / translationSpeed;
 
        while (elapsedTime < duration)
        {
            float k = elapsedTime / duration;
 
            transform.position = Vector3.Lerp(startPos, endPos, k);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }
}