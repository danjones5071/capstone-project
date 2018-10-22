using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
	public enum Weapons {
		Inferno = 100,
		DoubleLaser = 200
	}

	public void Purchase( string item )
	{
		switch( item )
		{
			case "Inferno":
				References.global.gameMaster.AddToCurrency( -1 * (int)Weapons.Inferno );
				break;
			case "DoubleLaser":
				References.global.gameMaster.AddToCurrency( -1 * (int)Weapons.DoubleLaser );
				break;
			default:
				References.global.gameMaster.currency = 999;
				break;
		}
			
	}
}


public class item
{
	public string name;
	public int price;
}