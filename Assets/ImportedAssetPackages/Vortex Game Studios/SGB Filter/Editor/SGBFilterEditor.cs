using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor( typeof( SGBFilter ) )]
[CanEditMultipleObjects]
public class SGBFilterEditor : Editor {
	private SerializedObject _so;
	private SGBFilter Target {
		get { return (SGBFilter)target; }
	}

	void OnEnable() {
		_so = new SerializedObject( target );
	}

	public override void OnInspectorGUI() {
		Target.colorPalette = EditorGUILayout.ObjectField( "Color Palette", Target.colorPalette, typeof( Texture ), false ) as Texture2D;
		Target.ditheringReference = EditorGUILayout.ObjectField( "Dithering Scale Reference", Target.ditheringReference, typeof( Texture ), false ) as Texture2D;
		Target.ditheringPattern = EditorGUILayout.ObjectField( "Dithering Pattern", Target.ditheringPattern, typeof( Texture ), false ) as Texture2D;
		Target.ditheringScale = EditorGUILayout.FloatField( "Dithering Scale", Target.ditheringScale );
	
		GUILayout.Label( "SGB Filter v.2.0.0", EditorStyles.miniBoldLabel );
		EditorUtility.SetDirty( Target );
	}
}