using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SpawnAdInWorld : MonoBehaviour
{
    [SerializeField] private GameObject ad;
    [SerializeField] private GameObject houseObject;

    [Header("Player Camera")]
    [SerializeField] private GameObject _camera;

    //The Initialized SpawnedAd 
    private GameObject spawnedAd;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        _audio.Play();
        spawnedAd = SpawnAd();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Not Triggered");
        _audio.Stop();
        DespawnAd(spawnedAd);
    }

    /// <summary>
    /// This is only for getting the ad to face the player in real time when spawned
    /// </summary>
    private void Update()
    {        //Make sure it faces the camera 
        if (spawnedAd.activeSelf)
        {
            spawnedAd.transform.LookAt(_camera.transform);
        }
    }

    /// <summary>
    /// Spawn an instance of an ad. This ad will later be returned such that we can destroy 
    /// these ads upon exiting this trigger.
    /// </summary>
    /// <returns></returns>
    private GameObject SpawnAd()
    {
        Vector3 currentPos = houseObject.transform.position;
        Vector3 adPos = new Vector3(currentPos.x, currentPos.y + 1f, currentPos.z);

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
