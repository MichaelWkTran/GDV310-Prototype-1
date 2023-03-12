Shader "Custom/ToonStyle"
{
	//   Properties
	//   {
	//       //Properties that can be adjusted through the inspector window 
	//       _Color("Color", Color) = (0.5, 0.65, 1, 1)
	//       _MainTex("Texture", 2D) = "white" {}
	//       [HDR]
	//       _AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
	//       _SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)
	//       _Glossiness("Glossiness", Float) = 32
	//       _RimColor("Rim Color", Color) = (1, 1, 1, 1)
	//       _RimAmount("Rim Amount", Range(0, 1)) = 0.716
	//       _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	//   }
	//   SubShader
	//   {
	//       Tags 
	//       { 
	//           //Declaring what pipeline to use 
	//           "RenderType" = "Opaque" 
	//           "RenderPipeline" = "UniversalRenderPipeline" 
	//       }
	//
	//       Pass
	//       {
	//           HLSLPROGRAM
	//           #pragma vertex vert
	//           #pragma fragment frag
	//
	//           // The Core.hlsl file contains definitions of frequently used HLSL
	//           // macros and functions, and also contains #include references to other
	//           // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
	//           //#include "Packages/com.unity.render-pipelines.high-definition/ShaderLibrary/Core.hlsl"
	//           #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"
	//           #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ImageBasedLighting.hlsl"
	//
	//           // The structure definition defines which variables it contains.
	//           // This example uses the Attributes structure as an input structure in
	//           // the vertex shader.
	//           struct Attributes
	//           {
	//           float4 positionOS : POSITION; //Object space position
	//           float2 uv : TEXCOORD0;
	//           float3 normal : NORMAL;
	//           };
	//           
	//           struct Varyings
	//           {
	//               // The positions in this struct must have the SV_POSITION semantic.
	//               float4 positionHCS  : SV_POSITION;
	//               float2 uv :TEXCOORD0;
	//               float3 worldNormal : NORMAL;
	//               float3 viewDir : TEXCOORD1;
	//               //SHADOW_COORDS(2)
	//           };
	//
	//           TEXTURE2D(_MainTex);
	//           SAMPLER(sampler_MainTex);
	//
	//           CBUFFER_START(UnityPerMaterial)
	//           half4 _Color;
	//           half4 _AmbientColor;
	//           half4 _SpecularColor;
	//           float _Glossiness;
	//           float4 _MainTex_ST;
	//           CBUFFER_END
	//
	//           struct Light
	//           {
	//               half3   direction;
	//               half3   color;
	//               half    distanceAttenuation;
	//               half    shadowAttenuation;
	//           };
	//           
	//           Light GetMainLight()
	//           {
	//              Light light;
	//              light.direction = _MainLightPosition.xyz;
	//              light.distanceAttenuation = unity_LightData.z; // unity_LightData.z is 1 when not culled by the culling mask, otherwise 0.
	//              light.shadowAttenuation = 1.0;
	//              light.color = _MainLightColor.rgb;
	//          
	//              return light;
	//           }
	//
	//           // The vertex shader definition with properties defined in the Varyings 
	//           // structure. The type of the vert function must match the type (struct)
	//           // that it returns.
	//
	//           Varyings vert(Attributes IN)
	//           {
	//               Varyings OUT;
	//               OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
	//               OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
	//               OUT.worldNormal = TransformObjectToWorldNormal(IN.normal);
	//               OUT.viewDir = GetWorldSpaceViewDir(OUT.positionHCS);
	//
	//               return OUT;
	//           }
	//
	//           // The fragment shader definition.            
	//           half4 frag(Varyings IN) : SV_Target
	//           {
	//               // Defining the color variable and returning it.
	//               float3 normal = normalize(IN.worldNormal);
	//               float NdotL = dot(GetMainLight().direction, normal);
	//               //float shadow = SHADOW_ATTENUATION(i);
	//               half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
	//
	//               float lightIntensity = smoothstep(0, 0.01, NdotL); //* shadow
	//               
	//               //float4 light = lightIntensity * _LightColor0;
	//
	//               //float3 viewDir = normalize(IN.viewDir);
	//               //float3 halfVector = normalize(GetMainLight().direction + viewDir);
	//               //float NdotH = dot(normal, halfVector);
	//
	//               //float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
	//               //float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
	//               //float4 specular = specularIntensitySmooth * _SpecularColor;
	//
	//               //float4 rimDot = 1 - dot(viewDir, normal);
	//               //float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);
	//               //float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
	//               //rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
	//               //float4 rim = rimIntensity * _RimColor;
	//
	//
	//               return _Color * tex * (_AmbientColor + lightIntensity); //+ specularIntensity);
	//               //return _Color * tex * (_AmbientColor + light + specular + rim);
	//               
	//               
	//               half4 color = tex * _Color;
	//
	//               return color;
	//           }
	//           ENDHLSL
	//       }
	//   }
	//
}