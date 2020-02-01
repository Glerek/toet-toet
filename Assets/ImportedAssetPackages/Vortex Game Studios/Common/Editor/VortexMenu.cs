using UnityEngine;
using UnityEditor;
using System.Collections;

public class VortexMenu : EditorWindow {
	[MenuItem( "Window/Vortex Game Studios/Our Assets", false, 1000 )]
	public static void OpenAssetStore() {
		Application.OpenURL( "https://goo.gl/E5JTf5" );
	}

	[MenuItem( "Window/Vortex Game Studios/About Us", false, 1000 )]
	public static void OpenSupportForm() {
		Application.OpenURL( "http://goo.gl/098wj8" );
	}
}
