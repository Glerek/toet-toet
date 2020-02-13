using System;
using UnityEngine;

[Serializable]
public class Pair<T, U>
{
	[SerializeField]
	private T _first = default(T);

	[SerializeField]
	private U _second = default(U);

	public Pair()
	{
	}

	public Pair(T first, U second)
	{
		this.First = first;
		this.Second = second;
	}

	public T First { get; set; }
	public U Second { get; set; }
};