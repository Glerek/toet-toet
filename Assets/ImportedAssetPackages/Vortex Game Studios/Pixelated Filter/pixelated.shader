Shader "Vortex Game Studios/Filters/Pixelated" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_pixelWidth ("Pixel Width", Float) = 1
		_pixelHeight ("Pixel Height", Float) = 1
		_pixelSize ("Pixel Size", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		pass { 
			CGPROGRAM
			#pragma target 2.0
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			fixed _pixelWidth;
			fixed _pixelHeight;
			fixed _pixelSize;

			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
			};

			float4 _MainTex_ST;
			fixed4 frag( v2f i ) : COLOR {
				if (_pixelSize <= 1.0) {
					fixed4 c = tex2D(_MainTex, i.uv);

					return c;
				} else {

					half2 tileCount = fixed2((_pixelWidth / (_pixelSize)), (_pixelHeight / (_pixelSize)));
					half2 tile = fixed2(1.0 / tileCount.x, 1.0 / tileCount.y);
					half2 halfTile = tile / 1.0;

					half2 tileUV = floor(i.uv / tile) * tile + halfTile;

					fixed4 c = tex2D(_MainTex, tileUV);

					return c;
				}
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}