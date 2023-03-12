Shader "Custom/ToonstyleCG"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

	}
		SubShader
	{
		Pass
		{
			Tags
			{
				"RenderType" = "Opaque"
				"LightMode" = "UniversalForward"
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase	
			#include "UnityCG.cginc"


			struct Attributes //
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct Varyings //
			{
				float4 positionHCS : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};

			//
			sampler2D _MainTex;
			float4 _MainTex_ST;

			//Object Color Values
			float4 _Color;
			float4 _AmbientColor;
			//float4 _Glossiness;

			Varyings vert(Attributes IN) //
			{
				Varyings OUT;
				OUT.positionHCS = UnityObjectToClipPos(IN.positionOS);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
				OUT.worldNormal = UnityObjectToWorldNormal(IN.normal);
				OUT.viewDir = WorldSpaceViewDir(IN.positionOS);
				return OUT;
			}

			float4 frag(Varyings IN) : SV_TARGET
			{
				float3 normal = normalize(IN.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float4 image = tex2D(_MainTex, IN.uv);

				float lightIntensity = lightIntensity = smoothstep(0, 0.01, NdotL);


				float4 totalLightValue = _AmbientColor + lightIntensity;
				return _Color * image * totalLightValue;
			}
			ENDCG
		}
	}
}