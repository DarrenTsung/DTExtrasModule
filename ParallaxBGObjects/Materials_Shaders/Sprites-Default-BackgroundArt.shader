Shader "Sprites/Default-BackgroundArt"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_MaxDepth ("Max Depth", float) = 100.0
		_ColorBlendScale ("Color Blend Scale", float) = 1.0
		_ColorToBlendTo ("Color To Blend To", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float fogDepth  : TEXCOORD1;
			};
			
			fixed4 _Color;
			float _MaxDepth;
			float _ColorBlendScale;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				float4 worldPos = mul(_Object2World, IN.vertex);
				float depth = clamp(worldPos.z / _MaxDepth, 0.0f, 1.0f);
				OUT.fogDepth = depth;
				
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			fixed4 _ColorToBlendTo;

			fixed4 frag(v2f IN) : SV_Target
			{
				float2 uv = IN.texcoord;
				fixed4 c = tex2D(_MainTex, uv) * IN.color;
				c = fixed4(lerp(c.rgb, _ColorToBlendTo.rgb, IN.fogDepth * _ColorBlendScale), c.a);
				
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
