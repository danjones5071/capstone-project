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

    //Time variables to keep phase duration
    private float startTimePhase;
    private float secondsElapsedPhase;

    // Use this for initialization
    void Start ()
	{
        currentPhase = 0;
        phaseCount = 0;
        startTimePhase = Time.time;
		StartCoroutine( PhaseTimer() );
	}

	IEnumerator PhaseTimer()
	{
		while( true )
		{
			yield return new WaitForSeconds( phaseDuration );
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
        // Increment phase count
        phaseCount++;

        // Increase appropriate Phase Multiplier
        currentPhase = phaseCount % 5;
        phaseMultipliers[currentPhase]++;
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