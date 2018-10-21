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
	public CurrencyGenerator currencyGenerator;
	public SoundEffects soundEffects;
	public UI_Manager uiManager;
	public GameObject playAgainUI;
	public GameObject storeUI;
	public GameObject pauseUI;
	public EnemyGenerator enemyGenerator;
    public PickupGenerator pickupGenerator;

	// No need to show these in the inspector. Can be derived from above.
	[HideInInspector] public Transform playerTrans;
	[HideInInspector] public Rigidbody2D playerRigid;
	[HideInInspector] public PlayerController playerController;

	void Awake()
	{
		global = this;

		playerTrans = player.transform;
		playerRigid = player.GetComponent<Rigidbody2D>();
		playerController = player.GetComponent<PlayerController>();
	}
}
