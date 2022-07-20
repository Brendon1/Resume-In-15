using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAdInWorld : MonoBehaviour
{

    [SerializeField] private GameObject ad;
    [SerializeField] private GameObject houseObject;
    [SerializeField] private GameObject player;

    private GameObject spawnedAd;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        spawnedAd = SpawnAd();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Not Triggered");
        DespawnAd(spawnedAd);
    }

    private void FixedUpdate()
    {
        //This is only for getting the ad to face the player in real time when spawned
        if (spawnedAd.activeSelf)
        {
            spawnedAd.transform.LookAt(player.transform.position);
        }
    }

    /// <summary>
    /// Spawn an instance of an ad. This ad will later be returned such that we can destroy 
    /// these ads upon exiting this trigger.
    /// </summary>
    /// <returns></returns>
    private GameObject SpawnAd()
    {
        List<GameObject> adsSpawned = new List<GameObject>();

        Vector3 currentPos = houseObject.transform.position;
        Vector3 adPos = new Vector3(currentPos.x, currentPos.y + 1, currentPos.z);
        GameObject _ad = Instantiate(ad, adPos, Quaternion.identity);

        return _ad;
    }

    /// <summary>
    /// Destroy the spawnedAd instantiated by "SpawnAd" function
    /// </summary>
    /// <param name="currentAds"></param>
    private void DespawnAd(GameObject currentAd)
    {
        Destroy(currentAd);
    }
}
