using UnityEngine;

public class ForegroundElement : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _renderer = null;
	public SpriteRenderer Renderer { get { return _renderer; } }
}