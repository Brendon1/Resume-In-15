using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISegmentedRotation : MonoBehaviour
{
    public GameObject player;
    public float distanceFromCamera = 1.0f;
    public float quadrantSize = 90f; // How many degrees in each UI quadrant
    public AnimationCurve MoveCurve;
    private Vector3 UIPositionOffset;
    private float animationCurveTime;

    // Start is called before the first frame update
    void Start()
    {
        ScaleCanvas();
        UIPositionOffset = new Vector3(0 , 0, distanceFromCamera);
    }

    private void Update() {
        Vector3 target = Camera.main.transform.position + getQuadrantVector();
        transform.position = Vector3.Slerp(transform.position, target, Time.deltaTime * 2);
        transform.rotation = getNewRotation();

        // TODO: fix animation curve for position slerp
        /*
        if(target != transform.position){
            animationCurveTime += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, target, animationCurveTime);
            transform.LookAt(Camera.main.transform);
        }
        else{
            animationCurveTime = 0;
        }*/
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
        int quadrant = (int) ((player.transform.rotation.eulerAngles.y + 0.5 * quadrantSize) % 360f / quadrantSize);
        float angle = quadrant * quadrantSize;

        return Quaternion.AngleAxis(angle, Vector3.up) * UIPositionOffset;
    }
}
