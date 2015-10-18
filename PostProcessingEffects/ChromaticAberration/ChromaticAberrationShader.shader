Shader "ImageEffects/ChromaticAberration" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AberrationOffset ("Chromatic Aberration Offset", Float) = 0
		_OffsetX ("Offset Direction X", Float) = 0
		_OffsetY ("Offset Direction Y", Float) = 0
		
		_ForwardChannelColor ("Forward Channel Color", Color) = (1,1,1,1)
		_BackChannelColor ("Back Channel Color", Color) = (1,1,1,1)
	}
	SubShader 
	{
		Pass
		{
		CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _AberrationOffset;
			
			uniform float _OffsetX;
			uniform float _OffsetY;
			
			uniform float4 _ForwardChannelColor;
			uniform float4 _BackChannelColor;
			
			float4 frag(v2f_img i) : COLOR
			{
				float2 coords = i.uv.xy;
				float2 offsetDirection = float2(_OffsetX, _OffsetY);
				
				_AberrationOffset /= 300.0f;
				float2 computedAberrationOffset = _AberrationOffset * offsetDirection;
				
				// Forward Channel
				float3 forwardChannel = tex2D(_MainTex, coords.xy - computedAberrationOffset).rgb * _ForwardChannelColor.rgb;
				// Back Channel
				float3 backChannel = tex2D(_MainTex, coords.xy + computedAberrationOffset).rgb * _BackChannelColor.rgb;
				
				float3 remainingChannelPercent = float3(max(0.0f, 1.0f - (_ForwardChannelColor.r + _BackChannelColor.r)),
																								max(0.0f, 1.0f - (_ForwardChannelColor.g + _BackChannelColor.g)),
																								max(0.0f, 1.0f - (_ForwardChannelColor.b + _BackChannelColor.b)));
				
				float3 remainingChannel = tex2D(_MainTex, coords.xy).rgb * remainingChannelPercent;
				float4 finalColor = float4(forwardChannel + backChannel + remainingChannel, 1.0f);
				return finalColor;
			}

		ENDCG
		} 
	}
}