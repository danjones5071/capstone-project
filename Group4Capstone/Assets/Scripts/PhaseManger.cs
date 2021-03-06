﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManger : MonoBehaviour
{
    public float phaseDuration = 5;  // Duration of each phase in seconds.

    public int[] phaseMultipliers = { 0, 0, 0, 0, 0 };

	public ParticleSystem nebulaParticles;

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
     */
    public void NextPhase()
    {
        // Increment phase count
        phaseCount++;

        // Increase appropriate Phase Multiplier
        currentPhase = phaseCount % 5;

		// Don't exceed 3 instances of Enemy Type A.
		// Don't exceed 4 instances of Enemy Type B.
		switch( currentPhase )
		{
			case 0:
				phaseMultipliers[currentPhase]++;
				break;
			case 1:
				if( phaseMultipliers[currentPhase] < 4 )
					phaseMultipliers[currentPhase]++;
				break;
			case 2:
				if( phaseMultipliers[currentPhase] < 3 )
					phaseMultipliers[currentPhase]++;
				break;
			case 3:
				break;
			default:
				phaseMultipliers[currentPhase]++;
				break;
		}
    }

	IEnumerator WaitForEvent()
	{
		int rand = 0;
		ObstacleGenerator obstacleGenerator = References.global.obstacleGenerator;
		EnemyGenerator enemyGenerator = References.global.enemyGenerator;
		UI_Manager uiManager = References.global.uiManager;
		PlayerController playerController = References.global.playerController;

		string asteroidBelt = "[Approaching Asteroid Belt]";
		string nebula = "[Approaching Nebula]";

		// Use a seed for better randomization.
		Random.InitState( System.Environment.TickCount );

		while( true )
		{
			yield return new WaitForSeconds( eventTimer );

			// If there's not already an event going on...
			if( !eventFlag )
			{
				rand = Random.Range( 0, 3 );
				// 0 = Asteroid Belt
				// 1 = Nebula
				// 2 = None (33% chance of no event)

				switch( rand )
				{
					case 0:
						obstacleGenerator.asteroidBelt = true;
						enemyGenerator.generate = false;
						eventFlag = true;
						uiManager.ShowEventNotification( asteroidBelt );
						yield return new WaitForSeconds( eventDuration );
						obstacleGenerator.asteroidBelt = false;
						if( References.global.player.activeInHierarchy )
							enemyGenerator.generate = true;
						eventFlag = false;
						break;
					case 1:
						nebulaParticles.Clear();
						nebulaParticles.Play();
						eventFlag = true;
						uiManager.ShowEventNotification( nebula );
						yield return new WaitForSeconds( 4 );
						float origEnergy = playerController.maxEnergy;
						playerController.maxEnergy = origEnergy / 2.0f;
						playerController.energy = Mathf.Min( playerController.energy , origEnergy / 2.0f );
						uiManager.nebula = true;
						yield return new WaitForSeconds( eventDuration );
						playerController.maxEnergy = origEnergy;
						uiManager.nebula = false;
						eventFlag = false;
						break;
					default:
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