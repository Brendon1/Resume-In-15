using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdCountdown : MonoBehaviour
{
    [SerializeField] private Image radialCountdown;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float startTime;

    private float currentCountdown;
    private bool countdownIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        currentCountdown = startTime;
        countdownIsRunning = true;

        radialCountdown.fillAmount = 1.0f;
        countdownText.text = Mathf.FloorToInt(currentCountdown).ToString();
        StartCoroutine(UpdateTimer());
    }

    // TODO: figure out performance issues or delete this section
    /*void Update()
    {
        if(countdownIsRunning)
        {
            currentCountdown -= Time.deltaTime;

            if(currentCountdown > 0.0f)
            {
                radialCountdown.fillAmount = Mathf.Clamp(currentCountdown / startTime, 0.0f, 1.0f);
                countdownText.text = Mathf.FloorToInt(currentCountdown).ToString();
            }
            else
            {
                radialCountdown.fillAmount = 0.0f;
                countdownText.text = "";
                countdownIsRunning = false;
            }
        }
    }*/

    private IEnumerator UpdateTimer()
    {
        while(currentCountdown >= 0)
        {
            countdownText.text = Mathf.FloorToInt(currentCountdown).ToString();
            radialCountdown.fillAmount = Mathf.InverseLerp(0, startTime, currentCountdown);
            currentCountdown--;

            yield return new WaitForSeconds(1.0f);
        }

        countdownText.text = "X";
        GetComponent<AdInteraction>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}
