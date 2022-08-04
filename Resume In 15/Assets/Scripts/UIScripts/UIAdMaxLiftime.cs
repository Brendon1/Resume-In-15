using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAdMaxLiftime : MonoBehaviour
{
    public float maxLifetime = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy(){
        yield return new WaitForSeconds(maxLifetime);
        Destroy(gameObject);
    }
}
