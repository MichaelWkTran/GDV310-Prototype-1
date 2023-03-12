Shader "Renderers/TestRenderer"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
    }

        HLSLINCLUDE

#pragma target 4.5
#pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch

        // #pragma enable_d3d11_debug_symbols

        //enable GPU instancing support
#pragma multi_compile_instancing
#pragma multi_compile _ DOTS_INSTANCING_ON

        ENDHLSL

        SubShader
    {
        Pass
        {
            Name "FirstPass"
            Tags { "LightMode" = "FirstPass" }

            Blend Off
            ZWrite Off
            ZTest LEqual

            Cull Back

            HLSLPROGRAM

        // List all the attributes needed in your shader (will be passed to the vertex shader)
        // you can see the complete list of these attributes in VaryingMesh.hlsl
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT

        // List all the varyings needed in your fragment shader
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TANGENT_TO_WORLD

        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassRenderers.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/VertMesh.hlsl"

        float4 _ColorMap_ST;
        float4 _Color;

        PackedVaryingsType Vert(AttributesMesh inputMesh)
        {
            VaryingsType varyingsType;
            varyingsType.vmesh = VertMesh(inputMesh);
            return PackVaryingsType(varyingsType);
        }

        float4 Frag(PackedVaryingsToPS packedInput) : SV_Target
        {

            float4 outColor = float4(0.0, 0.2, 0.8, 1.0);
            return outColor;
        }

        #pragma vertex Vert
        #pragma fragment Frag

        ENDHLSL
        }
    }
}
