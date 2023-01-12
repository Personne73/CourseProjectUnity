using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutines : MonoBehaviour
{
    // Coroutine : méthode à l'intérieur de laquelle on peut sortir et mettre en pause la méthode puis y revenir plus tard
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitCoroutine(2));
    }

    IEnumerator WaitCoroutine(float waitDuration)
    {
        Debug.Log("START WaitCoroutine");
        yield return new WaitForSeconds(waitDuration);
        Debug.Log("END WaitCoroutine");
    }
}
