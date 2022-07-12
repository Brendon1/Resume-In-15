using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdWorldSpawn : MonoBehaviour
{
    [SerializeField] private GameObject ad;
    [SerializeField] private GameObject player;
    private List<GameObject> adsSpawned = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        adsSpawned = SpawnAds();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Not Triggered");
        DespawnAds(adsSpawned);
    }

    private void FixedUpdate()
    {
        //This is only for getting the ad to face the player in real time when spawned
        if(adsSpawned.Count > 0)
        {
            foreach (GameObject ad in adsSpawned)
            {
                ad.transform.LookAt(player.transform.position);
            }
        }
    }

    /// <summary>
    /// Grab the children of this trigger so we can spawn ads above them
    /// </summary>
    /// <returns></returns>
    private List<Vector3> GetAllChildrenPos()
    {
        List<Vector3> children = new List<Vector3>();
        foreach (Transform child in transform)
        {
            children.Add(child.position);
        }

        return children;
    } 

    /// <summary>
    /// Spawn an instance of an ad over all children objects of this trigger. This list of ads
    /// will later be returned such that we can destroy these ads upon exiting this trigger.
    /// </summary>
    /// <returns></returns>
    private List<GameObject> SpawnAds()
    {
        List<Vector3> childrenPos = GetAllChildrenPos();
        List<GameObject> adsSpawned = new List<GameObject>();
        foreach (Vector3 obj in childrenPos)
        {
            Vector3 adPos = new Vector3(obj.x, obj.y + 1, obj.z);
            GameObject _ad = Instantiate(ad, adPos, Quaternion.identity);
            adsSpawned.Add(_ad);
        }

        return adsSpawned;
    }

    /// <summary>
    /// Destroy the current instantiated by "SpawnAds" function
    /// </summary>
    /// <param name="currentAds"></param>
    private void DespawnAds(List<GameObject> currentAds)
    {
        foreach (GameObject ad in currentAds)
        {
            Destroy(ad);
        }
        currentAds.Clear();
    }
}
