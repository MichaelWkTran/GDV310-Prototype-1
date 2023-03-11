Shader "Unlit/Rotating"
{
    Properties
    {
        //Properties which are changeable in the editor
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", color) = (1,1,1,1)
        _Rotation("Rotation", Float) = 0
        _RotationSpeed("RotationSpeed", Float) = 1
        _Closed("Closed", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"


            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _RotationSpeed;
            float _Rotation;
            float4 _Color;
            float _Closed;

            v2f vert (Attributes v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = o.uv * 2 - 1;

                float cosMat = cos(_Rotation + _Time.y * _RotationSpeed);
                float sinMat = sin(_Rotation + _Time.y * _RotationSpeed);
                float2x2 rotMatrix = float2x2(cosMat, -sinMat, sinMat, cosMat);

                o.uv = mul(rotMatrix, o.uv);
                o.uv = (o.uv + 1) * 0.5;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                if (_Closed != 0)
                    col = abs(1 - tex2D(_MainTex, i.uv));
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col * _Color;
            }
            ENDCG
        }
    }
}
