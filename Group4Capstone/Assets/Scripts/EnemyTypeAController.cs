using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeAController : MonoBehaviour {

    //public EnemyAI enemyAIScript;

	// Use this for initialization
	void Start ()
    {
        // Need to be able to set player location but can't access script
        // because script is in main Assets folder.
        //enemyAIScript = GetComponent<EnemyAI>();
        //enemyAIScript.playerLocation = References.global.playerTrans;
        //enemyAIScript.enemyArea = GameObject.Find("EnemyArea");
	}
	
}
