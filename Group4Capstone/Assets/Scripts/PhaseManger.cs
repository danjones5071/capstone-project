using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManger : MonoBehaviour
{
    public float phaseDuration = 5;  // Duration of each phase in seconds.

    public int[] phaseMultipliers = { 0, 0, 0, 0, 0 };

    // Phase tracking variables
    [SerializeField] private int phaseCount;
    [SerializeField] private int currentPhase;

	private bool eventFlag = false;
	private float eventTimer = 60;
	private float eventDuration = 30;

    // Use this for initialization
    void Start ()
	{
        currentPhase = 0;
        phaseCount = 0;
		StartCoroutine( PhaseTimer() );
		StartCoroutine( WaitForEvent() );
	}

	IEnumerator PhaseTimer()
	{
		while( true )
		{
			yield return new WaitForSeconds( phaseDuration );

			// Don't move on to next phase if an event is occuring.
			if( !eventFlag )
				NextPhase();
		}
	}

    /* Phase Layout
     * Phase 0: Asteroids
     * Phase 1: Type B Enemies
     * Phase 2: Type A Enemies
     * Phase 3: Blackholes
     * Phase 4: Event
     */
    public void NextPhase()
    {
		Debug.Log( "Entering phase: " + phaseCount );
        // Increment phase count
        phaseCount++;

        // Increase appropriate Phase Multiplier
        currentPhase = phaseCount % 5;
        phaseMultipliers[currentPhase]++;
    }

	IEnumerator WaitForEvent()
	{
		int rand = 0;
		ObstacleGenerator obstacleGenerator = References.global.obstacleGenerator;
		EnemyGenerator enemyGenerator = References.global.enemyGenerator;
		UI_Manager uiManager = References.global.uiManager;

		string asteroidBelt = "[Approaching Asteroid Belt]";
		string nebula = "[Approaching Nebula]";

		while( true )
		{
			yield return new WaitForSeconds( eventTimer );

			// If there's not already an event going on...
			if( !eventFlag )
			{
				rand = Random.Range( 0, 3 );

				switch( rand )
				{
					case 0:
						obstacleGenerator.asteroidBelt = true;
						enemyGenerator.generate = false;
						eventFlag = true;
						uiManager.ShowEventNotification( asteroidBelt );
						yield return new WaitForSeconds( eventDuration );
						obstacleGenerator.asteroidBelt = false;
						enemyGenerator.generate = true;
						eventFlag = false;
						break;
				}
			}
		}
	}

    public int getCurrentPhase()
    {
        return currentPhase;
    }

	public void setCurrentPhase( int phase )
	{
		currentPhase = phase;
	}

    public int getPhaseCount()
    {
        return phaseCount;
    }

	public void setPhaseCount( int count )
	{
		phaseCount = count;
	}
}