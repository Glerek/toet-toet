using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SubsystemData", menuName = "ScriptableObjects/Subsystem/Create Data", order = 2)]
public class SubsystemData : ScriptableObject
{
    public enum SubsystemType
    {
        Wheel = 0,
        Light,

        Count
    }

    [SerializeField]
    private SubsystemType _type =　SubsystemType.Count;
    public SubsystemType Type { get { return _type; } }


    [SerializeField]
    private Sprite _icon = null;
	public Sprite Icon { get { return _icon; } }
}
