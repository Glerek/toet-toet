Shader "Custom/VHSRewind"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SecondaryTex ("Secondary Texture", 2D) = "white" {}
		_SpeedX ("Speed Movement X", float) = 0.0
		_SpeedY ("Speed Movement Y", float) = 0.0
		_OffsetSpeedX ("Offset Speed Movement X", float) = 0.0
		_OffsetSpeedY ("Offset Speed Movement Y", float) = 0.0
		_OffsetDistortion ("Offset Distortion", float) = 500
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			half _SpeedX;
			half _SpeedY;
			half _OffsetSpeedX;
			half _OffsetSpeedY;

			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				o.uv2 = v.texcoord + frac(_Time.y * float2(_SpeedX + _OffsetSpeedX, _SpeedY + _OffsetSpeedY));
				return o;
			}

			sampler2D _MainTex;
			sampler2D _SecondaryTex;
			half _OffsetDistortion;

			fixed4 frag (v2f i) : SV_Target
			{
				i.uv = float2(frac(i.uv.x + cos((i.uv.y + _CosTime.y) * 100) / _OffsetDistortion), i.uv.y);
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_SecondaryTex, i.uv2);
				return lerp(col, col2, ceil(col2.r - 0.47));
			}
			ENDCG
		}
	}
}