// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32546,y:32568|spec-3-OUT,emission-53-OUT,alpha-114-OUT;n:type:ShaderForge.SFN_Color,id:2,x:32979,y:32187,ptlb:Tint,ptin:_Tint,glob:False,c1:0.04692906,c2:0.3881937,c3:0.4558824,c4:1;n:type:ShaderForge.SFN_Slider,id:3,x:33234,y:32605,ptlb:Specular,ptin:_Specular,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:4,x:32979,y:32361,ptlb:Bubble Texture,ptin:_BubbleTexture,tex:906306aff471ddb42a1ad07454a6bf93,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:10,x:32998,y:32785|EXP-59-OUT;n:type:ShaderForge.SFN_Add,id:53,x:32807,y:32287|A-2-RGB,B-4-RGB;n:type:ShaderForge.SFN_Slider,id:59,x:33187,y:32804,ptlb:Fresnel Power,ptin:_FresnelPower,min:0,cur:2,max:5;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:114,x:32797,y:32932|IN-10-OUT,IMIN-115-OUT,IMAX-117-OUT,OMIN-118-OUT,OMAX-120-OUT;n:type:ShaderForge.SFN_Vector1,id:115,x:32998,y:32911,v1:0;n:type:ShaderForge.SFN_Vector1,id:117,x:32998,y:32966,v1:1;n:type:ShaderForge.SFN_Slider,id:118,x:33028,y:33036,ptlb:AlphaFloor,ptin:_AlphaFloor,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:120,x:33028,y:33136,ptlb:AlphaCeil,ptin:_AlphaCeil,min:0,cur:1,max:1;proporder:3-2-4-59-118-120;pass:END;sub:END;*/

Shader "Shader Forge/Bubble1" {
    Properties {
        _Specular ("Specular", Range(0, 1)) = 1
        _Tint ("Tint", Color) = (0.04692906,0.3881937,0.4558824,1)
        _BubbleTexture ("Bubble Texture", 2D) = "white" {}
        _FresnelPower ("Fresnel Power", Range(0, 5)) = 2
        _AlphaFloor ("AlphaFloor", Range(0, 1)) = 0
        _AlphaCeil ("AlphaCeil", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Tint;
            uniform float _Specular;
            uniform sampler2D _BubbleTexture; uniform float4 _BubbleTexture_ST;
            uniform float _FresnelPower;
            uniform float _AlphaFloor;
            uniform float _AlphaCeil;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
////// Emissive:
                float2 node_134 = i.uv0;
                float4 node_4 = tex2D(_BubbleTexture,TRANSFORM_TEX(node_134.rg, _BubbleTexture));
                float3 emissive = (_Tint.rgb+node_4.rgb);
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0.0, dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                finalColor += specular;
                finalColor += emissive;
                float node_10 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower);
                float node_115 = 0.0;
/// Final Color:
                return fixed4(finalColor,(_AlphaFloor + ( (node_10 - node_115) * (_AlphaCeil - _AlphaFloor) ) / (1.0 - node_115)));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Tint;
            uniform float _Specular;
            uniform sampler2D _BubbleTexture; uniform float4 _BubbleTexture_ST;
            uniform float _FresnelPower;
            uniform float _AlphaFloor;
            uniform float _AlphaCeil;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0.0, dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                finalColor += specular;
                float node_10 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower);
                float node_115 = 0.0;
/// Final Color:
                return fixed4(finalColor * (_AlphaFloor + ( (node_10 - node_115) * (_AlphaCeil - _AlphaFloor) ) / (1.0 - node_115)),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
