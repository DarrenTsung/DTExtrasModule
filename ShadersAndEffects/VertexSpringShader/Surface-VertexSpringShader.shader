Shader "Custom/Surface-VertexSpringShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SmallestVertexY ("Smallest Vertex Y", Float) = 0.0
		_Height ("Height", Float) = 1.0
		_SpringValueX ("Spring Value X", Float) = 0.0
		_SpringValueZ ("Spring Value Z", Float) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		// Use vertex function "vert" 
		#pragma surface surf Standard fullforwardshadows vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _SmallestVertexY;
		float _Height;
		
		float _SpringValueX;
		float _SpringValueZ;
		
		void vert (inout appdata_full v) {
			float3 pos = mul(_Object2World, v.vertex).xyz;
			float heightFactor = min(pos.y - _SmallestVertexY, _Height) / _Height;
			
			float squaredMagnitude = (_SpringValueX * _SpringValueX)  + (_SpringValueZ * _SpringValueZ);
			
			float4 vertexOffset = float4(_SpringValueX * heightFactor, 0.0f, _SpringValueZ * heightFactor, 0.0f);
			v.vertex += mul(_World2Object, vertexOffset);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
