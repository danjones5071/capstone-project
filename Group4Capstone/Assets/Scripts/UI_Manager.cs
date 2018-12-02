using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Manager : MonoBehaviour {
   
    private float lastFrameTime;
    private float secondsElapsed;
    private string timeText = string.Empty;
    private bool gameOver = false;

    [SerializeField]
    private Text gameTimeElapsedRef;

    [SerializeField]
    private Text totalTimeSurvived;

	public Text currencyCount;
	public Text LivesCount;
	public Text weaponText;
	public GameObject eventIndicator;
	private Text eventIndicatorText;

    public Slider energyBar;
    public Slider healthBar;

	public bool nebula = false;

	private PlayerController playerController;

    // Use this for initialization
    void Start ()
    {  
		InitializeUIPages();
        lastFrameTime = Time.time;
		eventIndicatorText = eventIndicator.GetComponent<Text>();
		playerController = References.global.playerController;
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Clock Logic
        if (!gameOver)
        {
            secondsElapsed += Time.time - lastFrameTime;

            int minutes = ((int)secondsElapsed / 60);
            int seconds = ((int)(secondsElapsed % 60));

            string temp = "0" + minutes;
            string minutesString = (minutes < 10 ? temp : minutes.ToString());

            temp = "0" + seconds;
            string secondsString = (seconds < 10 ? temp : seconds.ToString());

            timeText = (minutesString + ":" + secondsString);

            gameTimeElapsedRef.text = timeText;

            lastFrameTime = Time.time;
        }
        #endregion

        #region Health Bar Logic
		healthBar.value = playerController.health;
        #endregion

        #region Energy Bar Logic
		float energy = (playerController.energy / playerController.maxEnergy) * 100.0f;
		if( !nebula )
			energyBar.value = energy;
		else
			energyBar.value = energy / 2.0f;

		Debug.Log( "Energy: " + playerController.energy );
		Debug.Log( "Energy Bar: " + energyBar.value );
        #endregion

    }

	// Ensure all unnecessary UI pages like pause and store screens are disabled at first.
	public void InitializeUIPages()
	{
		eventIndicator.SetActive( false );
		References.global.storeUI.SetActive( false );
		References.global.pauseUI.SetActive( false );
		References.global.playAgainUI.SetActive( false );
		References.global.leaderboardUI.SetActive( false );
	}

	public void ShowEventNotification( string eventText )
	{
		eventIndicatorText.text = eventText;
		StartCoroutine( DisplayEventNotification() );
	}

	IEnumerator DisplayEventNotification()
	{
		eventIndicator.SetActive( true );
		yield return new WaitForSeconds(4);
		eventIndicator.SetActive( false );
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

	public void TogglePauseUI()
	{
		GameObject pauseUI = References.global.pauseUI;
		pauseUI.SetActive( !pauseUI.activeInHierarchy );
	}

	public void UpdateCurrencyCount( int amount )
	{
		currencyCount.text = "x " + amount;
	}

	public void UpdateLivesCount( int lifeCount )
	{
		LivesCount.text = "x " + lifeCount;
	}

	public void SetWeaponText( string text )
	{
		weaponText.text = text;
	}

    public void ContinueGame()
    {
        lastFrameTime = Time.time;
        gameOver = false;
    }
}
