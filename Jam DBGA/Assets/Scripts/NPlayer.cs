using UnityEngine;
using System.Collections;

public class NPlayer : MonoBehaviour 
{	
	public int nPlayer;

	protected static NPlayer _self;
	public static NPlayer Self
	{
		get
		{
			if (_self == null)
				_self = FindObjectOfType(typeof(NPlayer)) as NPlayer;
			return _self;
		}
	}

	void Awake () 
	{
		//DontDestroyOnLoad (this.gameObject);
		if (_self != null && _self != this) 
		{ 
			Destroy(this.gameObject); 
		} 
		else 
		{ 
			_self = this;
			DontDestroyOnLoad(this.gameObject);
		} 

	}
}
