Shader "Custom/test2"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_AlphaTex("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : SV_Target;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : SV_Target;
			};

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float4 _MainTex_ST;
			float4 _AlphaTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = tex2D(_MainTex, IN.uv) * IN.color;
				half4 alphaColor = tex2D(_AlphaTex, IN.uv);
				color.a = (alphaColor.r + alphaColor.g + alphaColor.b) / 3.0;
				return color;
			}
			ENDCG
		}
	}
}
