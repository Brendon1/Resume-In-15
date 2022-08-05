using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI mouseSensitivityText;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivityText.text = $"{slider.value}";
        slider.onValueChanged.AddListener((v) => mouseSensitivityText.text = v.ToString());
    }
}
