/*
 * 
 * SGBFilter.shader 
 * Version 1.00
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
public class PixelatedFilter : FilterBehavior {

	public float pixelSize = 1.0f;

	void OnRenderImage( RenderTexture source, RenderTexture destination ) {
		source.filterMode = FilterMode.Point;

		this.material.SetFloat( "_pixelSize", (float)( pixelSize ) );
		this.material.SetFloat( "_pixelWidth", (float)( source.width ) );
		this.material.SetFloat( "_pixelHeight", (float)( source.height ) );

		Graphics.Blit( source, destination, this.material );
	}
}