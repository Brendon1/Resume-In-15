using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("PlayerUI GameObjects")]
    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private RectTransform crosshair;
    [SerializeField]
    private Image crosshairImage;

    //Crosshair parameters
    private readonly float normalSize = 10f;
    private readonly float maxSize = 40;
    private readonly float speed = 25f;
    private float currentSize;

    [Header("List of Tasks")]
    [SerializeField] private TextMeshProUGUI task1;
    [SerializeField] private TextMeshProUGUI task2;
    [SerializeField] private TextMeshProUGUI task3;
    [SerializeField] private TextMeshProUGUI task4;
    [SerializeField] private TextMeshProUGUI task5;

    [Header("All File Tasks (Task 6)")]
    [SerializeField] private TextMeshProUGUI task6;
    [SerializeField] private TextMeshProUGUI File1;
    [SerializeField] private TextMeshProUGUI File2;
    [SerializeField] private TextMeshProUGUI File3;
    [SerializeField] private TextMeshProUGUI File4;
    [SerializeField] private TextMeshProUGUI File5;

    //[SerializeField] private TextMeshProUGUI task7;
    [SerializeField] private GameObject lastTask;

    //Checks for completion status
    private readonly float opacityOfCompletion = 0.2f;

    /// <summary>
    /// Updates the Text and the Size of the Crosshair for when Hovering over interactables.
    /// Refer to its use in "PlayerInteraction" Script.
    /// </summary>
    /// <param name="promptString"></param>
    public void UpdateTextAndCrosshair(string promptString)
    {
        promptText.text = promptString;

        //Check if there's currently a prompt on screen
        if (string.IsNullOrEmpty(promptString))
        {
            currentSize = Mathf.Lerp(currentSize, normalSize, speed * Time.deltaTime);
            crosshairImage.color = Color.white;
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, speed * Time.deltaTime);
            crosshairImage.color = Color.cyan;
        }

        crosshair.sizeDelta = new Vector2(currentSize, currentSize);
    }

    public bool AllPreviousTasksComplete()
    {
        if (task1.color.a == opacityOfCompletion && task2.color.a == opacityOfCompletion && task3.color.a == opacityOfCompletion && task4.color.a == opacityOfCompletion && task5.color.a == opacityOfCompletion && task6.color.a == opacityOfCompletion)
        {
            lastTask.SetActive(true);
            return true;
        }

        return false;
    }

    public bool FinishedLastTask()
    {
        if(AllPreviousTasksComplete() && lastTask.GetComponent<TextMeshProUGUI>().color.a == opacityOfCompletion)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Helper Method to keep track of files obtained
    /// </summary>
    public void AllFilesObtained()
    {
        if (File1.color.a == opacityOfCompletion && File2.color.a == opacityOfCompletion && File3.color.a == opacityOfCompletion && File4.color.a == opacityOfCompletion && File5.color.a == opacityOfCompletion)
        {
            Color currentTextOpacity = task6.color;
            currentTextOpacity.a = 0.2f;
            task6.color = currentTextOpacity;
        }
    }
}
