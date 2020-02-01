Shader "Vortex Game Studios/Filters/SGB Effect" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
		_ReferenceTex("Reference (RGB)", 2D) = "white" {}
		_PaletteTex("Palette (RGB)", 2D) = "white" {}
		_DitheringTex("Dithering Pattern (RGB)", 2D) = "white" {}

		_DiteringWidth("Dithering Width", float) = 1.0
		_DiteringHeight("Dithering Height", float) = 1.0
	}

	SubShader {
		Tags {
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Pass {
			ZTest Always Cull Off ZWrite Off

			CGPROGRAM
			#pragma target 2.0
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			sampler2D _ReferenceTex;
			sampler2D _PaletteTex;
			sampler2D _DitheringTex;
			fixed _DiteringWidth;
			fixed _DiteringHeight;

			fixed4 frag(v2f_img i) : SV_Target {
				fixed4 output;
				
				fixed grayScale = dot(fixed3(0.2126, 0.7152, 0.0722), tex2D(_MainTex, i.uv).rgb);
				
				// Dithering
				fixed2 ditherUV = i.uv * fixed2(_DiteringWidth, _DiteringHeight);
				fixed ditherScale = tex2D(_ReferenceTex, fixed2(grayScale, 0.9)).g;
				fixed ditherTone = tex2D(_ReferenceTex, fixed2(grayScale, 0.1)).g;
				
				if (tex2D(_DitheringTex, ditherUV).g < ditherTone)
					output.rgb = tex2D(_PaletteTex, fixed2(ditherScale+0.31, 0.5)).rgb;
				else
					output.rgb = tex2D(_PaletteTex, fixed2(ditherScale, 0.5)).rgb;

				output.a = 1.0;
				return output;
			}
			ENDCG
		}
	}

	Fallback off
}
