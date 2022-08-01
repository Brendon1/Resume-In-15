using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISegmentedRotation : MonoBehaviour
{
    public GameObject player;
    public float distanceFromCamera = 1.0f;
    public float XQuadrantSize = 45f; // How many degrees in each vertical UI quadrant
    public float YQuadrantSize = 90f; // How many degrees in each horizontal UI quadrant

    // Start is called before the first frame update
    void Start()
    {
        ScaleCanvas();
    }

    private void Update() {
        Vector3 target = Camera.main.transform.position + getQuadrantVector();
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 15);
        transform.rotation = getNewRotation();
    }

    private Quaternion getNewRotation(){
        Vector3 direction = transform.position - Camera.main.transform.position;
        return Quaternion.LookRotation(direction);
    }

    private void ScaleCanvas(){
        transform.position = Camera.main.transform.position + (Camera.main.transform.forward * distanceFromCamera);

        float cameraHeight = 2.0f * distanceFromCamera * Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView * 0.5f));

        transform.localScale = new Vector3(cameraHeight / Screen.height, cameraHeight / Screen.height, 1);
    }

    private Vector3 getQuadrantVector(){
        int XAxisQuadrant = (int) ((Camera.main.transform.rotation.eulerAngles.x + 0.5 * XQuadrantSize) % 360f / XQuadrantSize);
        float angleX = XAxisQuadrant * XQuadrantSize;

        int YAxisQuadrant = (int) ((player.transform.rotation.eulerAngles.y + 0.5 * YQuadrantSize) % 360f / YQuadrantSize);
        float angleY = YAxisQuadrant * YQuadrantSize;

        var rotation = Quaternion.Euler(angleX, angleY, 0);

        return rotation * Vector3.forward * distanceFromCamera;
    }
}
