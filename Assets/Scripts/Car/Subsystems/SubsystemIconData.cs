using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SubsystemIconData", menuName = "ScriptableObjects/Subsystem/Create Icon Data", order = 1)]
public class SubsystemIconData : ScriptableObject
{
    [Serializable]
    public class IconData
    {
        public SubsystemData.SubsystemType Type;
        public Sprite Icon;
    }

    [SerializeField]
    private List<IconData> _iconData = new List<IconData>();
    public List<IconData> Icons { get { return _iconData; } }
}
