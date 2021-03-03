using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListBank : BaseListBank
{
	private string[] _contents = {
		"Fire", "Water", "Air", "Earth", "Lightning"
	};

	public override string GetListContent(int index)
	{
		return _contents[index].ToString();
	}

	public override int GetListLength()
	{
		return _contents.Length;
	}

	public void GetSelectedContentID(int contentID)
	{

		PlayerCombat player = GameObject.Find("Player").GetComponent<PlayerCombat>();
		player.ChooseSpellTarget(contentID);
	}
}
