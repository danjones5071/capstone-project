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

	public Text currencyCount;
	public Text weaponText;

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
		healthBar.value = References.global.playerController.health;
        #endregion

        #region Energy Bar Logic
		energyBar.value = References.global.playerController.batteryCapacity;
        #endregion

    }

	public void ShowPlayAgainUI()
	{
        gameOver = true;

        totalTimeSurvived.text = timeText;

		References.global.playAgainUI.SetActive( true );   
	}

	public void ToggleStoreUI()
	{
		GameObject storeUI = References.global.storeUI;
		storeUI.SetActive( !storeUI.activeSelf );
	}

	public void UpdateCurrencyCount( int amount )
	{
		currencyCount.text = "x " + amount;
	}

	public void SetWeaponText( string text )
	{
		weaponText.text = text;
	}
}
