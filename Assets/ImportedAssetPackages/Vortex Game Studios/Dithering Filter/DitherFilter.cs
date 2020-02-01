/*
 * 
 * DitherFilter.cs
 * Version 1.00
 *
 * [25/07/2015]
 * The first build, have fun ^^
 * 
 * Developed by Vortex Game Studios LTDA ME. (http://www.vortexstudios.com)
 * Authors:		Alexandre Ribeiro de Sá (@themonkeytail)
 *				Luiz Fernando Ribeiro de Sá
 * 
 */

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DitherFilter : FilterBehavior {
	public Texture2D ditheringReference = null;
	public Texture2D ditheringPattern = null;
	public float ditheringScale = 1.0f;

	void OnRenderImage( RenderTexture source, RenderTexture destination ) {
		source.filterMode = FilterMode.Point;

		if ( ditheringReference != null )
			this.material.SetTexture( "_ReferenceTex", ditheringReference );
		if ( ditheringPattern != null )
			this.material.SetTexture( "_DitheringTex", ditheringPattern );

		this.material.SetFloat( "_DiteringWidth", (float)(source.width / ( ditheringPattern.width * ditheringScale )) );
		this.material.SetFloat( "_DiteringHeight", (float)(source.height / ( ditheringPattern.height * ditheringScale )) );

		Graphics.Blit( source, destination, this.material );
	}
}
