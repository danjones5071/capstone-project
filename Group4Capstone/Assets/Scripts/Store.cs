using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
	public List<Item> weapons = new List<Item>()
	{
		new Item( References.WNAME_INFERNO, 100 ),
		new Item( References.WNAME_2LASER, 200 )
	};

	void Awake()
	{
		weapons.Add( new Item( References.WNAME_INFERNO, 100 ) );
		weapons.Add( new Item( References.WNAME_2LASER, 200 ) );
	}

	public void Purchase( string itemName )
	{
		Item item = weapons.Find( i => i.name == itemName );

		if( !References.global.playerController.weapons.Contains( item.name ) )
		{
			References.global.playerController.weapons.Add( item.name );
			References.global.gameMaster.AddToCurrency( -1 * item.price );
		}
		else
		{
			Debug.Log( "Item Already Purchased!" );
		}
	}
}


public class Item
{
	public string name;
	public int price;

	public Item( string name, int price )
	{
		this.name = name;
		this.price = price;
	}
}