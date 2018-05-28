﻿Shader "Custom/EmissionShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("NoiseTexture", 2D) = "white" {}
		_Amplitude("Amplitude", Float) = 100
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 worldPosition : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _NoiseTexture;
			float _Amplitude;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 offset = float2(
					tex2D(_NoiseTexture, float2(i.worldPosition.y + _Time[1], 0)).r,
					tex2D(_NoiseTexture, float2(0, i.worldPosition.x + _Time[1])).r
					);

				offset -= 0.5;

				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 emission = tex2D(_NoiseTexture, i.uv + offset);
				emission.a = 0;
				col += emission * abs(sin(_Time[1])) * _Amplitude;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
