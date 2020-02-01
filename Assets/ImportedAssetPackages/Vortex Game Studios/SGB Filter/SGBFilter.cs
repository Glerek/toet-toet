/*
 * 
 * SGBFilter.shader 
 * Version 2.0.0
 *
 * [10/02/2016]
 * The entire shader file has been recoded. 
 * Every math has been "pre-calculated" inside a texture file. Doing that we can make the 
 * filter much more light on mobiles devices.
 *
 * [25/07/2015]
 * The first build, have fun ^^
 * 
 * Developed by Vortex Game Studios LTDA ME. (http://www.vortexstudios.com)
 * Authors:		Alexandre Ribeiro de Sá (@alexribeirodesa)
 *				Luiz Fernando Ribeiro de Sá
 * 
 */

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SGBFilter : FilterBehavior {
	//public Material SGBMaterial;
	public Texture2D colorPalette = null;
	public Texture2D ditheringReference = null;
	public Texture2D ditheringPattern = null;
	public float ditheringScale = 1.0f;

	void OnRenderImage( RenderTexture source, RenderTexture destination ) {
		source.filterMode = FilterMode.Point;
		if ( colorPalette != null )
			this.material.SetTexture( "_PaletteTex", colorPalette );
		if ( ditheringReference != null )
			this.material.SetTexture( "_ReferenceTex", ditheringReference );
		if ( ditheringPattern != null )
			this.material.SetTexture( "_DitheringTex", ditheringPattern );

		

		this.material.SetFloat( "_DiteringWidth", (float)(source.width / ( ditheringPattern.width * ditheringScale )) );
		this.material.SetFloat( "_DiteringHeight", (float)(source.height / ( ditheringPattern.height * ditheringScale )) );

		Graphics.Blit( source, destination, this.material );
	}
}
