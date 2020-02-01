using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor( typeof( DitherFilter ) )]
[CanEditMultipleObjects]
public class DitherFilterEditor : Editor {
	private SerializedObject _so;
	private DitherFilter Target {
		get { return (DitherFilter)target; }
	}

	void OnEnable() {
		_so = new SerializedObject( target );
	}

	public override void OnInspectorGUI() {
		Target.ditheringReference = EditorGUILayout.ObjectField( "Dithering Scale Reference", Target.ditheringReference, typeof( Texture ), false ) as Texture2D;
		Target.ditheringPattern = EditorGUILayout.ObjectField( "Dithering Pattern", Target.ditheringPattern, typeof( Texture ), false ) as Texture2D;
		Target.ditheringScale = EditorGUILayout.FloatField( "Dithering Scale", Target.ditheringScale );

		GUILayout.Label( "Dithering Filter v.2.0.0", EditorStyles.miniBoldLabel );
		EditorUtility.SetDirty( Target );
	}
}