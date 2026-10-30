using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private GameController gc;
    public VideoPlayer player;
    
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        player.SetDirectAudioVolume(0, gc.optionsSettings.GetEffectsVolume()); 
    }

}
