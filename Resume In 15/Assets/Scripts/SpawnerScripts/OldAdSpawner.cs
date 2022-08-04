using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldRandomAdSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform target;
    public float radius;
    public float spawnTime; // how frequently objects spawn
    private float currentTimeToSpawn; // time until next spawn

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // call spawner every X interval of time
        if(currentTimeToSpawn > 0){
            currentTimeToSpawn -= Time.deltaTime;
        }
        else{
            SpawnObject(target.position);
            currentTimeToSpawn = spawnTime;
        }
    }

    // spawn the object
    public void SpawnObject(Vector3 centerPoint){
        // get random point in sphere
        Vector3 pointOnShpere = GetPointOnSphere(centerPoint, radius);

        // instantiate new ad object at point
        GameObject adObj = (GameObject)Instantiate(objectToSpawn, pointOnShpere, Quaternion.identity);
        adObj.transform.LookAt(target);

        //adObj.GetComponent<AdFollow>().setTarget(target, pointOnShpere);
    }

    // generate random sphere point
    private Vector3 GetPointOnSphere(Vector3 sphereCenter, float radius){
        // generate two rand numbers between 0 and 1
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);

        // use rand numbers to get spherical coordinates theta and phi
        float theta = 2 * Mathf.PI * u;
        float phi = Mathf.Acos(2 * v - 1);

        float temp = phi * Mathf.Rad2Deg;

        // convert from spherical coordinates to cartesian coordinates
        // y and z conversions are swapped to account for unity's coordinate system
        float x = sphereCenter.x + radius * Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = sphereCenter.y + radius * Mathf.Cos(phi);
        float z = sphereCenter.z + radius * Mathf.Sin(phi) * Mathf.Sin(theta);

        return new Vector3(x, y, z);
    }

}
