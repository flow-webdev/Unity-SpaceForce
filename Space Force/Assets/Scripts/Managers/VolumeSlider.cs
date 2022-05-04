using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
    }
}