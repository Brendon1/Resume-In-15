using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdButtonSpawner : MonoBehaviour
{
    public float spawnTime = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnButton());
    }

    IEnumerator SpawnButton(){
        yield return new WaitForSeconds(spawnTime);
        Transform child = transform.Find("adButton");
        child.gameObject.SetActive(true);
    }
}
