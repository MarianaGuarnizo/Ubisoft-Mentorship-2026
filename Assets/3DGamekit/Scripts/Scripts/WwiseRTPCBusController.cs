using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WwiseRTPCBusController : MonoBehaviour
{
    [Header("RTPC Name From Wwise")]
    public string rtpcName = "MasterVolume";

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("No Slider component found on this GameObject.");
            return;
        }

        slider.onValueChanged.AddListener(SetVolume);

        // Apply initial value
        SetVolume(slider.value);
    }

    public void SetVolume(float value)
    {
        float rtpcValue = value * 100f; // Convert 0-1 slider to 0-100 RTPC
        AkSoundEngine.SetRTPCValue(rtpcName, rtpcValue);
    }
}