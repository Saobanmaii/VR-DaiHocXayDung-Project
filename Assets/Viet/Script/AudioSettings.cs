using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
           
            SetVolume(volumeSlider.value);
        }
    }

    
    public void SetVolume(float sliderValue)
    {
        // Chuyển đổi giá trị tuyến tính của Slider sang Logarithmic (Decibel)
        // Nhân 20 vì công thức tính dB: dB = 20 * log10(amplitude)
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        
 
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetVolume(volumeSlider.value);
    }
}