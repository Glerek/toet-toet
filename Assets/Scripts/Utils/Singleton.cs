using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class PrefabAttribute : Attribute
{
	string _name;
	public string Name { get { return this._name; } }
	public PrefabAttribute() { this._name = ""; }
	public PrefabAttribute(string name) { this._name = name; }
}

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance = null;
	public static bool IsAwake { get { return (_instance != null); } }

	public static T Instance
	{
		get
		{
			Type mytype = typeof(T);
			if (_instance == null)
			{
				_instance = (T)FindObjectOfType(mytype);
				if (_instance == null)
				{
					string goName = mytype.ToString();
					GameObject go = GameObject.Find(goName);
					if (go == null)
					{
						go = GameObject.Find(goName + "(Clone)");
					}

					if (go == null)
					{
						bool hasPrefab = Attribute.IsDefined(mytype, typeof(PrefabAttribute));
						if (hasPrefab)
						{
							PrefabAttribute attr = (PrefabAttribute)Attribute.GetCustomAttribute(mytype, typeof(PrefabAttribute));
							string prefabname = attr.Name;
							try
							{
								if (prefabname != "")
								{
									go = (GameObject)Instantiate(Resources.Load(prefabname, typeof(GameObject)));
								}
								else
								{
									go = (GameObject)Instantiate(Resources.Load(goName, typeof(GameObject)));
								}
							}
							catch (Exception e)
							{
								Debug.LogError("Could not instantiate prefab even though prefab attribute was set.\n" + e.Message + "\n" + e.StackTrace);
							}
						}

						if (go == null)
						{
							go = new GameObject();
							go.name = goName;
						}
					}

					_instance = go.GetComponent<T>();
					if (_instance == null)
					{
						_instance = go.AddComponent<T>();
					}

					DontDestroyOnLoad(go);
				}
				else
				{
					int count = FindObjectsOfType(mytype).Length;
					if (count > 1)
					{
						throw new Exception("Too many (" + count.ToString() + ") prefab singletons of type: " + mytype.Name);
					}
				}
			}

			return _instance;
		}
	}

	public virtual void OnApplicationQuit()
	{
		_instance = null;
	}
}
