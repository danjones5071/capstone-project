using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class References : MonoBehaviour
{
	public static References global { get; private set; }

	// Assets that can be modified in the inspector.
	public GameMaster gameMaster;
	public GameObject player;
	public ObstacleGenerator obstacleGenerator;
	public SoundEffects soundEffects;
	public UI_Manager uiManager;
	public GameObject playAgainUI;
	public GameObject storeUI;
	public GameObject pauseUI;
	public EnemyGenerator enemyGenerator;
    public PickupGenerator pickupGenerator;
    public PhaseManger phaseManager;
	public Leaderboard leaderboard;

	// No need to show these in the inspector. Can be derived from above.
	[HideInInspector] public Transform playerTrans;
	[HideInInspector] public Rigidbody2D playerRigid;
	[HideInInspector] public PlayerController playerController;

	// Weapon Name Constants.
	public const string WNAME_LASER = "Laser";
	public const string WNAME_INFERNO = "Inferno";
	public const string WNAME_2LASER = "Double Laser";

    public enum GamePhases
    {
        AsteroidPhase, // Might need to start enumeration at 1
        TypeBEnemyPhase,
        TypeAEnemyPhase,
        BlackholePhase,
        EventPhase
    }

	void Awake()
	{
		global = this;

		playerTrans = player.transform;
		playerRigid = player.GetComponent<Rigidbody2D>();
		playerController = player.GetComponent<PlayerController>();

		//phaseManager.setCurrentPhase( 0 );
		//phaseManager.setPhaseCount( 0 );
		//Array.Clear( phaseManager.PhaseMultipliers, 0, phaseManager.PhaseMultipliers.Length );
	}
}
