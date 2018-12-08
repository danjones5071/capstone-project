using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
	public Transform buttonGroupTrans;
	public Text nameText;
	public Text priceText;
	public Text descText;

	private List<Item> items = new List<Item>()
	{
		new Item( References.WNAME_INFERNO, 100, "This projectile weapon is firey fast! But it is extinguished by black holes." ),
		new Item( References.WNAME_2LASER, 200, "Shoot two standard lasers at once." ),
		new Item( References.UNAME_ARMOR, 200, "Increase your maximum health by 50%." )
	};

	private List<string> purchased = new List<string>();
	private GameMaster gameMaster;

	void Start()
	{
		// Cache necessary components.
		gameMaster = References.global.gameMaster;
	}

	void OnEnable()
	{
		// Make sure already purchased items have their purchase buttons disabled.
		foreach( Transform child in buttonGroupTrans )
		{
			if( purchased.Contains( child.name ) )
			{
				child.GetComponent<Button>().interactable = false;
			}
		}
	}

	public void Purchase( string itemName )
	{
		Item item = items.Find( i => i.name == itemName );

		// If the player has enough money and hasn't bought the item already.
		if( !purchased.Contains( item.name ) && item.price < gameMaster.Currency )
		{
			// Take currency from player.
			References.global.gameMaster.AddToCurrency( -1 * item.price );

			// Add to our PlayerPrefs data for persistent storage.
			PlayerPrefs.SetInt( item.name, 1 );

			// Add to our list of purchased items.
			purchased.Add( item.name );

			// Disable the button in the store;
			buttonGroupTrans.Find( item.name ).GetComponent<Button>().interactable = false;

			References.global.soundEffects.PlayCurrencyPickUpSound();
		}
		else if( item.price > gameMaster.Currency )
		{
			priceText.text = "Insufficient!";
		}
	}

	public void DisplayDetails( string itemName )
	{
		Item item = items.Find( i => i.name == itemName );

		if( item != null )
		{
			nameText.text = item.name;
			priceText.text = item.price.ToString();
			descText.text = item.description;
		}
	}

	public void ClearDetails()
	{
		nameText.text = "";
		priceText.text = "";
		descText.text = "";
	}

	public void AddToPurchasedList( string name )
	{
		purchased.Add( name );
	}

	public List<Item> GetItemsList()
	{
		return( items );
	}
}



public class Item
{
	public string name;
	public int price;
	public string description;

	public Item( string name, int price, string description )
	{
		this.name = name;
		this.price = price;
		this.description = description;
	}
}