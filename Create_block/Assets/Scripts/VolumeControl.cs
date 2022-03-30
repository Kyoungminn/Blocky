using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource == null)
        {
            audioSource = GameObject.Find("Network Player(Clone)").GetComponentInChildren<AudioSource>();
            volumeSlider.value = audioSource.volume;
        }
    }

    public void VolumeUp()
    {
        if (audioSource.volume < 1.0f)
        {
            audioSource.volume += 0.1f;
            volumeSlider.value += 0.1f;
        }
    }

    public void VolumeDown()
    {
        if (audioSource.volume > 0.0f)
        {
            audioSource.volume -= 0.1f;
            volumeSlider.value -= 0.1f;
        }
    }
}