using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
	public enum Weapons {
		Inferno = 100
	}

	public void Purchase( string item )
	{
		switch( item )
		{
			case "Inferno":
				Debug.Log( "purchase inferno" );
				References.global.gameMaster.AddToCurrency( -1 * (int)Weapons.Inferno );
				break;
		}
			
	}
}


public class item
{
	public string name;
	public int price;
}