﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Manager : MonoBehaviour {
   
    private float startTime;
    private float secondsElapsed;

    [SerializeField]
    private Text gameTimeElapsedRef;

    public Slider energyBar;
    public Slider healthBar;

    // Use this for initialization
    void Start ()
    {  
        startTime = Time.time;      
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Clock Logic
        secondsElapsed = Time.time - startTime;

        int minutes = ((int)secondsElapsed / 60);
        int seconds = ((int)(secondsElapsed % 60));

        string temp = "0" + minutes;
        string minutesString = (minutes < 10 ? temp : minutes.ToString());

        temp = "0" + seconds;
        string secondsString = (seconds < 10 ? temp : seconds.ToString());

        gameTimeElapsedRef.text = (minutesString + ":" + secondsString);
        #endregion

        #region Energy Bar Logic
        energyBar.value = (float)PlayerController.batteryCapacity;
        #endregion
    }
}
