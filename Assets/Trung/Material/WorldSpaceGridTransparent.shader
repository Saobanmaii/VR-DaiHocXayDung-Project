Shader "Custom/WorldSpaceGridTransparent"
{
    Properties
    {
        _GridTex ("Grid Mask Texture", 2D) = "white" {}
        _Color ("Grid Color", Color) = (0, 1, 0.6, 0.6)
        _Density ("Grid Density", Float) = 2
        _Alpha ("Alpha", Range(0, 1)) = 0.6
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _GridTex;
            float4 _Color;
            float _Density;
            float _Alpha;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Plane dựng đứng thường dùng XY.
                // Nếu sai hướng, đổi xy thành xz hoặc yz.
                float2 uv = i.worldPos.yz * _Density;

                fixed mask = tex2D(_GridTex, uv).r;

                fixed4 col = _Color;
                col.a = mask * _Alpha;

                return col;
            }
            ENDCG
        }
    }
}