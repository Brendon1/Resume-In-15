using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RandomAdSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject parent;
    public Vector2 adSize = new Vector2(300, 300);
    public int maxNumAds = 20;
    public float onLaunchSpawnRate = 4.0f;
    public float startingSpawnRate = 8.0f;
    public float minSpawnRate = 2.0f;
    private float timeUntilSpawn;
    public float changeSpawnTimer = 8.0f;
    private float timeUntilChangeSpawn;
    private List<Object> adsList;

    // Start is called before the first frame update
    void Start()
    {
        // Read in all ad videos
        adsList = new List<Object>(Resources.LoadAll("AdVideos"));
        timeUntilSpawn = onLaunchSpawnRate;
        timeUntilChangeSpawn = changeSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0){
            SpawnObject();
            timeUntilSpawn = startingSpawnRate;
            Debug.Log("spawn rate is: " + startingSpawnRate);
        }

        if(startingSpawnRate > minSpawnRate){
            timeUntilChangeSpawn -= Time.deltaTime;

            if(timeUntilChangeSpawn <= 0){
                timeUntilChangeSpawn = changeSpawnTimer;
                startingSpawnRate = startingSpawnRate * 0.85f;
            }
        }
    }

    // Instantiate an advertisement in the UI canvas
    void SpawnObject(){
        if(parent.transform.childCount >= maxNumAds){
            return;
        }

        // Instantiate new advertisement object
        GameObject adObj = (GameObject)Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
        adObj.transform.SetParent(parent.transform, false);

        // Get a random spawn point in the canvas
        Vector2 spawnPoint = getRandomSpawnPoint();

        // Adjust position of ad obj
        adObj.GetComponent<RectTransform>().anchoredPosition = spawnPoint;

        // Adjust size of ad obj
        adObj.GetComponent<RectTransform>().sizeDelta = adSize;

        /* TODO: delete this chunk after sufficient testing of new code
        // Create new render texture
        //var rendTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //var rendTexture = new RenderTexture((int)adSize.x, (int)adSize.y, 24);

        // Add texture to ad obj raw image
        //adObj.GetComponent<RawImage>().texture = rendTexture;

        // Add texture to ad obj video player
        //adObj.GetComponent<VideoPlayer>().targetTexture = rendTexture;
        */

        // Select random video from list
        int index = Random.Range(0, adsList.Count);

        // add video clip to video player
        adObj.GetComponent<VideoPlayer>().clip = (VideoClip)adsList[index];
        float videoWidth = adObj.GetComponent<VideoPlayer>().clip.width;
        float videoHeight = adObj.GetComponent<VideoPlayer>().clip.height;

        // Create new render texture
        var rendTexture = new RenderTexture((int)videoWidth, (int)videoHeight, 24);

        // Add texture to ad obj raw image
        adObj.GetComponent<RawImage>().texture = rendTexture;

        // Add texture to ad obj video player
        adObj.GetComponent<VideoPlayer>().targetTexture = rendTexture;
    }

    // Generate a random spawn location within the UI
    Vector2 getRandomSpawnPoint(){
        Vector2 spawnRange = parent.GetComponent<RectTransform>().sizeDelta - adSize;

        Vector2 spawnPoint =  new Vector2();
        spawnPoint.x = Random.Range(-0.5f * spawnRange.x, 0.5f * spawnRange.x);
        spawnPoint.y = Random.Range(-0.5f * spawnRange.y, 0.5f * spawnRange.y);

        return spawnPoint;
    }
}
