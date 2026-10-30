using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    private GameController gc;

    public GameObject musicVolumeSlider;
    public GameObject effectsVolumeSlider;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

        musicVolumeSlider.GetComponent<Slider>().value = gc.optionsSettings.GetMusicVolume();
        effectsVolumeSlider.GetComponent<Slider>().value = gc.optionsSettings.GetEffectsVolume();
    }

    public void OnMusicVoulumeChange(float volume)
    {
		gc.UpdateMusicVolume(volume);
	}

	public void OnEffectsVoulumeChange(float volume)
    {
		gc.UpdateEffectsVolume(volume);
	}
}
