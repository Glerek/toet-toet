Shader "Vortex Game Studios/Filters/Dither Filter" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}

		_ReferenceTex("Reference (RGB)", 2D) = "white" {}
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
			sampler2D _DitheringTex;
			fixed _DiteringWidth;
			fixed _DiteringHeight;

			fixed4 frag(v2f_img i) : SV_Target {
				fixed4 output = tex2D(_MainTex, i.uv);

				// Dithering
				fixed2 ditherUV = i.uv * fixed2(_DiteringWidth, _DiteringHeight);

				// RED
				fixed ditherScale = tex2D(_ReferenceTex, fixed2(output.r, 0.9)).r;
				fixed ditherTone = tex2D(_ReferenceTex, fixed2(output.r, 0.1)).r;
				
				if (tex2D(_DitheringTex, ditherUV).g < ditherTone)
					output.r = tex2D(_ReferenceTex, fixed2(output.r + 0.31, 0.9));
				else
					output.r = ditherScale;

				// GREEN
				ditherScale = tex2D(_ReferenceTex, fixed2(output.g, 0.9)).g;
				ditherTone = tex2D(_ReferenceTex, fixed2(output.g, 0.1)).g;

				if (tex2D(_DitheringTex, ditherUV).g < ditherTone)
					output.g = tex2D(_ReferenceTex, fixed2(output.g + 0.31, 0.9)).g;
				else
					output.g = ditherScale;

				// BLUE
				ditherScale = tex2D(_ReferenceTex, fixed2(output.b, 0.9)).b;
				ditherTone = tex2D(_ReferenceTex, fixed2(output.b, 0.1)).b;

				if (tex2D(_DitheringTex, ditherUV).g < ditherTone)
					output.b = tex2D(_ReferenceTex, fixed2(output.b + 0.31, 0.9)).b;
				else
					output.b = ditherScale;
				
				return output;
			}
			ENDCG
		}
	}

	Fallback off
}
