using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Manager : MonoBehaviour {
   
    private float startTime;
    private float secondsElapsed;
    private string timeText = string.Empty;
    private bool gameOver = false;

    [SerializeField]
    private Text gameTimeElapsedRef;

    [SerializeField]
    private Text totalTimeSurvived;

    public Slider energyBar;
    public Slider healthBar;

    private GameObject player;
    private PlayerController playerController;

	public GameObject playAgainUI;


    // Use this for initialization
    void Start ()
    {  
        startTime = Time.time;

        // Cache player object
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Clock Logic
        if (!gameOver)
        {
            secondsElapsed = Time.time - startTime;

            int minutes = ((int)secondsElapsed / 60);
            int seconds = ((int)(secondsElapsed % 60));

            string temp = "0" + minutes;
            string minutesString = (minutes < 10 ? temp : minutes.ToString());

            temp = "0" + seconds;
            string secondsString = (seconds < 10 ? temp : seconds.ToString());

            timeText = (minutesString + ":" + secondsString);

            gameTimeElapsedRef.text = timeText;
        }
        #endregion

        #region Health Bar Logic
		healthBar.value = playerController.health;
        #endregion

        #region Energy Bar Logic
        energyBar.value = playerController.batteryCapacity;
        #endregion

    }

	public void ShowPlayAgainUI()
	{
        gameOver = true;

        totalTimeSurvived.text = timeText;

        playAgainUI.SetActive( true );   
	}
}
