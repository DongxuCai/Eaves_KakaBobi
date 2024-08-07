Shader "Custom/CelShader"
{
	Properties 
	{
		[Header(ShowAnimation)]
		[Toggle]_needAnimation("Need Animation Process",int) = 1
			//_AnimProcess("iii",float) = 0
		_RandomRange("Random Range",range(0,1)) = 0.5
		_Scale("Scale",Range(0,1)) = 0.2
		_Angle("Angle",Range(-1,1)) = 0.2
		_Height("Height",Range(-10,10)) = 5
		[Space(20)]

		[Header(Preview)]
		[Toggle]_needPreview("Need Preview",int) = 0
		_previewColor("Preview Color",color) = (1,1,1,0.5)
		[Space(20)]

		[Header(PlantGrow)]
		[Toggle]_trunkGrow("Trunk Grow",int) = 0
		[Toggle]_leafGrow("Leaf Grow",int) = 0
		[Enum (UnityEngine.Rendering.CullMode)] _cullMode("Cull Mode",int) = 0
		_GrowProcess("Grow Process",Range(0,1)) = 0
		_GrowTime("Grow Time",float) = 1
		_GrowDelayTime("Grow Delay Time",float) = 0
		_DampMinMax("DampMinMax",vector) = (0,0.2,0,0)
		_Expand("Grow Expand",float) = 0.05
		[Space(20)]

		[Header(PlantAnim)]
		[Toggle]_needPlantAnimation("Need Plant Animation",int) = 0
		[Toggle]_moveMaskUseUV("Move Mask Use UV",int) = 0
		//_RV ("Wind XYZDir & Power" , Vector) = (1, 0, 1, 0)
		_WindFactor("Wind Amplitude&Ymask",vector) = (1,0.2,1,0.2)
		//_WindMulFactor("Wind Multi",vector) = (1,1,1,1)
		[Space(20)]

		[Header(Bird)]
		[Toggle]_isBird("Is Bird",int) = 0
		_FlyWingsFactor("Fly Amplitude&Frequency&Density&Mask",vector) = (1,0.2,0.1,0.2)
		_FlyBodyFactor("Fly Body Amplitude&Delay",vector) = (1,0.2,0.1,0.2)
		[Space(20)]

		[Header(Collapse Animation)]
		_CollapseColor("Collapse Color", Color) = (1, 1, 1, 1)
		_CollapseColorPos("Collapse Color Grid",range(0,1)) = 0.5
		_CollapseColorSpeed("Collapse Color Speed",float) = 5
		_CollapseColorWidth("Collapse Color Width",float) = 5
		_CollapseWaveSpeed("Collapse Wave Speed",float) = 5
		_CollapseWaveWidth("Collapse Wave Width",float) = 5
		_CollapseWaveHeight("Collapse Wave Height",Range(0,10)) = 5
		[Space(20)]

		[Header(BaseColor)]
		_Color("Base Color", Color) = (1, 1, 1, 1)
		_ColorSec("Sec Color", Color) = (1, 1, 1, 1)
		_RandomColorValue("Random Color Value",Range(0,1)) = 0
		_MainTex("Texture", 2D) = "white" {}
		_needClip("Need Alpha Clip",float) = 0
		_FogGroundFade("Fog Ground Fade",float) = 50
		[Space(20)]

		[Header(Diffuse)]
		_RampLevels("Ramp Levels",int) = 1
		_LightScalar("Light Scalar",float) = 1
		_HighColor("High Color", Color) = (1, 1, 1, 1)
		_LowColor("Low Color", Color) = (1, 1, 1, 1)
		_HighIntensity("High Intensity",float) = 2.0
		_LowIntensity("Low Intensity",float) = 1.5
		[Space(20)]

		[Header(Normal)]
		[Toggle]_needNormal("Need NormalTex",int) = 0
		_NormalTex("Normal", 2D) = "bump" {}
		_NormalStrength("Normal Strength",Range(0,2)) = 1
		[Space(20)]

		[Header(Emission)]
		[Toggle]_isLight("isLight",int) = 0
		_EmissionTex("Emission", 2D) = "black" {}
		[HDR]_EmissionColor("Emission Color", Color) = (1, 1, 1, 1)
		[Space(20)]

		[Header(Ripple)]
        _RippleTex("RippleTex", 2D) = "white" {}		

		[Header(Rim)]
		_RimAlpha("Rim Alpha",float) = 0
		_RimDropOff("Rim DropOff",float) = 0
		_RimPower("Rim Power",float) = 0
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)

	}

	SubShader 
	{
		//必须是RenderType为Opaque 材质才写入DepthNormalTexture和DepthTexture。且要写在 SubShader 的下面，不能写在Pass里面
		Tags{"RenderType"="Opaque" "Queue" = "Geometry""ReplaceVertexColor"="true"}

		Cull [_cullMode]
		
		//多pass共享声明和代码部分
		CGINCLUDE
		#include "../../Shader/CelShader/CelShader.cginc"
		//#include "Assets/99_Shader/CelShader/CelShader.cginc"
		ENDCG
		

		
		Pass 
		{
			Tags{ "LightMode" = "ForwardBase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#define BASEPASS
			//#include "../../Resources/MyCelShader.cginc"
			
			v2f vert (a2v v) 
			{
				v2f o = (v2f)0;

				float4 worldPos = 0;
				VertexAnimation(v,o,worldPos);
				computePos(v,o,worldPos);
				computeNormal( v,o);
				shadowFog(v,o);

				return o;
			}

			fixed4 frag(v2f i): SV_TARGET 
			{
				float4 col;
				fragColor(i,col);
				
				return col;
			}

			ENDCG
		}



		//Addtional Pass
		Pass
		{
			//平行光之外的额外光源用ForwardAdd
			Tags{"LightMode" = "ForwardAdd"}
			
			//光照混合模式用oneone叠加
			Blend One One
			
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			// //multi_compile_fwdadd指令可以保证我们在shader中使用光照衰减等光照变量可以被正确赋值
			#pragma multi_compile_fwdadd
			#define ADDITIONALPASS
			//#include "../../Resources/MyCelShader.cginc"

			
			v2f vert(a2v v)
			{
				v2f o = (v2f)0;
				float4 worldPos = float4(1,1,1,1);
				VertexAnimation(v,o,worldPos);
				computePos(v,o,worldPos);

				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos.xyz));
				#ifdef USING_DIRECTIONAL_LIGHT
					fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				#else
					fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos.xyz);
				#endif
				fixed3 diffuse = _LightColor0.rgb * max(0, dot(worldNormal, worldLightDir));
				UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos.xyz);//point light & spot light
				
				float4 col = 0;
				clipFunction(i,col);

				return fixed4((diffuse) * atten, 1.0);
			}
			ENDCG
		}


		Pass 
		{
			Tags { "LightMode"="ShadowCaster" }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#define SHADOWPASS
			//#include "../../Resources/MyCelShader.cginc"

			v2f vert(a2v v)
			{
				v2f o = (v2f)0;
				float4 worldPos = 0;
				VertexAnimation(v,o,worldPos);

				#if _NEEDPLANTANIMATION_ON
					v.vertex.xyz = mul(unity_WorldToObject , worldPos).xyz;
				#endif

				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
				return o;
			}

			float4 frag( v2f i ) : SV_Target
			{
				float4 col = 0;
				clipFunction(i,col);
				SHADOW_CASTER_FRAGMENT(i)
			}

			ENDCG
		}
	}
}


