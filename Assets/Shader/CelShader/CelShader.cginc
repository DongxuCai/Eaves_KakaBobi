#ifndef CelShader
    #define CelShader

    #include "UnityCG.cginc"
    #include "Lighting.cginc"
    #include "AutoLight.cginc"

    #pragma shader_feature _ _NEEDANIMATION_ON 
    #pragma shader_feature _ _TRUNKGROW_ON
    #pragma shader_feature _ _LEAFGROW_ON
    #pragma shader_feature _ _NEEDPLANTANIMATION_ON
    #pragma shader_feature _ _ISBIRD_ON
    #pragma shader_feature _ _NEEDNORMAL_ON
    #pragma shader_feature _ _NEEDPREVIEW_ON

    #pragma multi_compile_fog
    #ifdef SHADOWPASS
        #pragma multi_compile_shadowcaster
    #endif 


    
    sampler2D _MainTex;
    uniform float4 _MainTex_ST;

    int _needPreview;
    float _isPreview;
    float4 _previewColor;

    float _AnimProcess,_Attenuation,_CollapseShow,_RandomRange,_Scale,_Angle,_Height;
    float _CollapseTime,_CollapseWaveAlpha;
    float _CollapseColorPos,_CollapseColorSpeed,_CollapseColorWidth,_CollapseWaveSpeed,_CollapseWaveWidth,_CollapseWaveHeight;
    float3 _CollapseWorldPos;
    float _needClip,_moveMaskUseUV;

    float _GrowProcess,_GrowTime,_GrowDelayTime,_Expand;
    float4 _DampMinMax;
    float4 _RV,_WindFactor,_WindMulFactor,_FlyWingsFactor,_FlyBodyFactor;
    float _CurrentTime;

    float easeInOutBack(float x) 
    {
        float c1 = 1.70158;
        float c2 = c1 * 1.525;

        return x < 0.5
        ?  2 * x * x
        : (pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
    }
    float easeInExpo(float x) 
    {
        return x * x * x * x;
    }
    float Remap(float In, float2 InMinMax, float2 OutMinMax)
    {
        return OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
    }
    float easySmoothStep(float MinValue,float MaxValue,float x)
    {
        return (x-MinValue)/(MaxValue-MinValue);
    }
    fixed3x3 rotateY(float process)
    {
        float value = lerp(_Angle*6.283,0,process);

        fixed3x3 R_y = 
        {
            cos(value),0,sin(value),
            0,1,0,
            -sin(value),0,cos(value),
        };
        return R_y;
    }

    float timeFlowSin(float baseValue,float speed,int sign)
    {
        return sin(frac(baseValue + _Time.y*speed*sign)*6.28);
    }

    float ScriptTimeSin(float baseValue)
    {
        return sin(baseValue*6.28 + (_CurrentTime)*6.28);
    }

    void RotateAboutAxis(float randomValue,float4 objPos,float moveMask,out float4 worldPos)
    {
        worldPos = mul(unity_ObjectToWorld, objPos);
        float4 worldPosSrc = worldPos;
        float4 RV = float4(lerp(normalize(_RV.xyz),-normalize(_RV.xyz),_moveMaskUseUV) , _RV.w);
        float3 ObjectPosition = mul(unity_ObjectToWorld, float4(0.0, 0.0, 0.0, 1.0)).xyz;
        float DotRV = dot(RV.xyz, worldPos.xyz - ObjectPosition);
        float3 ClosestPointOnAxis = RV.xyz * DotRV;
        float3 UAxis = (worldPos.xyz - ObjectPosition) - ClosestPointOnAxis;
        float3 VAxis = cross(RV.xyz, UAxis);

        float CosAngle;
        float SinAngle;
        float waveFrequency = ScriptTimeSin( randomValue)+ _RV.w;
        sincos(_WindFactor.x*waveFrequency*_WindMulFactor.x, SinAngle, CosAngle);
        float3 R = UAxis * CosAngle + VAxis * SinAngle;
        worldPos.xyz = ClosestPointOnAxis + R + ObjectPosition;
        float windMask = saturate((moveMask)/(_WindFactor.y*_WindMulFactor.z));
        
        worldPos.xyz = lerp(worldPosSrc.xyz,worldPos.xyz,windMask);

        //矫正法线
        // worldNormal = UnityObjectToWorldNormal(objNormal);
        // DotRV = dot(RV.xyz, worldNormal);
        // ClosestPointOnAxis = RV.xyz * DotRV;
        // UAxis = worldNormal - ClosestPointOnAxis;
        // VAxis = cross(RV.xyz, UAxis);
        // R = UAxis * CosAngle + VAxis * SinAngle;
        // worldNormal = ClosestPointOnAxis + R;
        
    }


    // struct a2v
    // {
        //         float4 vertex : POSITION;
        //         float2 uv : TEXCOORD0;
        //         float2 uv02 : TEXCOORD1;
        //         float3 normal : NORMAL;
        //         #ifdef BASEPASS
        //             float4 tangent : TANGENT;
        //         #endif 
        //         #ifdef VERTEXCOLPASS
        //             float4 color : COLOR;
        //         #endif
        
    // };

    // struct v2f
    // {
        //         #ifdef SHADOWPASS
        //             V2F_SHADOW_CASTER;
        //         #endif
        //         float4 uv : TEXCOORD2;
        //         #if defined (BASEPASS) || defined (ADDITIONALPASS) || defined (VERTEXCOLPASS)
        //             float4 pos : SV_POSITION;
        //             float3 worldNormal : TEXCOORD3;
        //             float4 worldPos : TEXCOORD4;
        //         #endif
        
        //         #ifdef BASEPASS
        //             SHADOW_COORDS(5)
        //             UNITY_FOG_COORDS(6)
        //             float3 worldTangent : TEXCOORD7;
        //             float3 worldBitangent : TEXCOORD8;
        //         #endif
        
        //         float4 center : TEXCOORD9;
        //         #ifdef VERTEXCOLPASS
        //             float4 color : COLOR;
        //         #endif
        
    // };

    struct a2v
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        float2 uv02 : TEXCOORD1;
        float3 normal : NORMAL;
        float4 tangent : TANGENT;
        float4 color : COLOR;
    };

    struct v2f
    {
        float4 uv : TEXCOORD0;
        float4 pos : SV_POSITION;
        SHADOW_COORDS(1)
        UNITY_FOG_COORDS(2)
        float3 worldNormal : TEXCOORD3;
        float3 worldTangent : TEXCOORD4;
        float3 worldBitangent : TEXCOORD5;
        float4 worldPos : TEXCOORD6;
        float4 center : TEXCOORD7;
        float4 color : COLOR;
    };


    void VertexAnimation(inout a2v v,inout v2f o,inout float4 worldPos)
    {
        o.uv.xy = v.uv.xy;
        o.uv.zw = v.uv02.xy;
        
        float3 center = unity_ObjectToWorld._14_24_34;
        o.center.xyz = center;

        #if _NEEDANIMATION_ON
            //随机值
            float4 localPos = v.vertex;
            float randomValue = frac(sin(dot(center.xz,float2(12.9898,78.233)))*43758.5453)*2-1;
            float animProcess = Remap(_AnimProcess,float2(0,1),float2(-_RandomRange,1+_RandomRange));
            animProcess = easeInOutBack(saturate(animProcess + randomValue*_RandomRange));

            //缩放
            localPos.xz = lerp(lerp(float2(0,0),localPos.xz,saturate(_Scale+_Attenuation)),localPos.xz,easeInExpo(animProcess));
            float3 worldPosTemp = mul(unity_ObjectToWorld , localPos.xyz).xyz;
            worldPosTemp.y = lerp(_Height,worldPosTemp.y,animProcess);
            localPos.xyz = mul(unity_WorldToObject , worldPosTemp).xyz;

            //旋转
            localPos.xyz = mul(rotateY(animProcess),localPos.xyz);

            //坍缩模拟
            float4 worldPos02 = mul(unity_ObjectToWorld , localPos);
            float dis = distance(worldPos02.xyz,_CollapseWorldPos)/_CollapseWaveWidth;
            dis = saturate((1 - cos(clamp(dis - _CollapseTime*_CollapseWaveSpeed,0,6.28)))*easeInExpo(_CollapseWaveAlpha));
            worldPos02.y += dis * _CollapseWaveHeight * saturate(worldPos02.y/5 - 0.3);
            v.vertex.xyz = mul(unity_WorldToObject , worldPos02).xyz;
        #endif

        #if _TRUNKGROW_ON
            float growMask01 = smoothstep(_DampMinMax.x,_DampMinMax.y,v.uv.y - _GrowProcess + _DampMinMax.z);
            float3 vertex_offset01 = growMask01 * v.normal * _Expand;
            v.vertex = v.vertex + float4( vertex_offset01,0);
            o.center.w = 1 - growMask01;
        #endif

        #if _LEAFGROW_ON
            float growMask02 = smoothstep(_DampMinMax.x,_DampMinMax.y,v.uv02.y - _GrowProcess );
            o.center.w = 1 - growMask02;
        #endif

        #if _NEEDPLANTANIMATION_ON
            float randomValue02 = frac(sin(dot(center.xz,float2(12.9898,78.233)))*43758.5453)*2-1;
            float moveMask = lerp(v.vertex.y,v.uv.y,_moveMaskUseUV);
            RotateAboutAxis(randomValue02,v.vertex,moveMask,worldPos);
        #endif

        #if _ISBIRD_ON
            float flyMask = easySmoothStep(0.5-_FlyWingsFactor.w,0,v.uv.x) * easySmoothStep(0.5+_FlyWingsFactor.w,1,v.uv.x);
            float flyWings = timeFlowSin(abs(v.uv.x - 0.5)*_FlyWingsFactor.z,_FlyWingsFactor.y,-1) * flyMask *_FlyWingsFactor.x;
            float flyBody = timeFlowSin(_FlyBodyFactor.y,_FlyWingsFactor.y,-1) *_FlyBodyFactor.x;
            v.vertex.y += flyWings + flyBody;
        #endif

        

    }


    void computePos(in a2v v,inout v2f o,in float4 worldPos)
    {
        o.worldPos = float4(mul(unity_ObjectToWorld , v.vertex).xyz,1);
        o.pos = UnityObjectToClipPos(v.vertex);
        o.worldNormal = UnityObjectToWorldNormal(v.normal);

        #if _NEEDPLANTANIMATION_ON
            o.pos = UnityWorldToClipPos(worldPos);
            o.worldPos = worldPos;
        #endif
    }


    void computeNormal(in a2v v,inout v2f o)
    {
        #if _NEEDNORMAL_ON
            o.worldTangent = normalize(UnityObjectToWorldNormal(v.tangent));
            o.worldBitangent = cross(o.worldTangent, o.worldNormal)* v.tangent.w * unity_WorldTransformParams.w;
        #endif
        
    }

    void shadowFog(in a2v v,inout v2f o)
    {
        TRANSFER_SHADOW(o);
        UNITY_TRANSFER_FOG(o,o.pos);
    }

    

    sampler2D _NormalTex;
    sampler2D _EmissionTex;
    uniform float4 _NormalTex_ST;
    uniform float4 _EmissionTex_ST;
    sampler2D _RippleTex;

    float _NormalStrength;
    int _RampLevels;
    float _LightScalar,_HighIntensity,_LowIntensity,_FogGroundFade;
    float4 _Color,_ColorSec,_HighColor,_LowColor;
    
    float _isLight,_LightIntensity,_RandomColorValue,_OrthographicCamSize,_RainOn;
    float _RimDropOff,_RimAlpha,_RimPower;
    float4 _RimColor,_CollapseColor,_EmissionColor;


    void fragColor(in v2f i,inout float4 col)
    {
        // Get view direction && light direction for rim lighting
        float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

        // Sample textures
        col = tex2D(_MainTex, i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw);
        float4 colSrc = col;

        // Get normal
        #if _NEEDNORMAL_ON
            fixed3 tangentNormal = tex2D(_NormalTex, i.uv.xy * _NormalTex_ST.xy + _NormalTex_ST.zw) * 2 - 1;
            tangentNormal *= _NormalStrength;
            float3x3 T2W = { i.worldTangent.xyz, i.worldBitangent.xyz, i.worldNormal.xyz };
            tangentNormal.z = pow((1 - pow(tangentNormal.x, 2) - pow(tangentNormal.y, 2)), 0.5);
            float3 worldNormal = normalize(mul(tangentNormal, T2W));
        #else
            float3 worldNormal = i.worldNormal.xyz;
        #endif
        

        // Get Emission
        float lightMask = saturate(1 - _isLight + _LightIntensity);
        float4 emmision = tex2D(_EmissionTex, i.uv.xy * _EmissionTex_ST.xy + _EmissionTex_ST.zw);
        
        // Base Lambert
        half factor = dot(viewDirection, worldNormal);

        // Shadow attenuation
        fixed shadow = SHADOW_ATTENUATION(i);
        float intensity = dot(worldNormal, lightDirection);
        intensity = saturate(intensity + _LightScalar);
        intensity *= shadow;
        
        // Lambert level
        float rampLevel = round(intensity * _RampLevels)* saturate(_WorldSpaceLightPos0.y);//修正阳光在地平线下的异常阴影
        float lightMultiplier = _LowIntensity + ((_HighIntensity - _LowIntensity) / (_RampLevels)) * rampLevel;
        float4 highColor = (rampLevel / _RampLevels) * _HighColor;
        float4 lowColor = ((_RampLevels - rampLevel) / _RampLevels) * _LowColor;
        float4 mixColor = (highColor + lowColor) / 2;
        
        // Mix
        col *= lightMultiplier;
        col *= lerp(_Color,_ColorSec,_RandomColorValue) * mixColor;

        // Apply hard rim lighting
        //_RimAlpha *= 1 - ((1 - rampLevel / _RampLevels) * (1 - _RimDropOff));
        //int rimMask = step(factor,_RimPower);
        //col.rgb = (_RimColor.rgb * _RimAlpha + col.rgb * (1 - _RimAlpha)) * rimMask + col.rgb * (1 - rimMask);

        // Apply emmision lighting
        emmision = lerp(col,emmision* _EmissionColor,emmision.r);
        col = lerp(col,emmision,lightMask);

        //Collapse
        float dis = distance(lerp(i.worldPos,i.center.xyz,_CollapseColorPos),_CollapseWorldPos)/_CollapseColorWidth;
        dis = saturate((1 - cos(clamp(dis - _CollapseTime*_CollapseColorSpeed,0,6.28)))*easeInExpo(_CollapseWaveAlpha))*_CollapseShow;

        col.rgb = lerp(col.rgb,_CollapseColor.rgb,dis*_CollapseColor.a);

        //浮沫RT uv匹配
        float2 RTuv = i.worldPos.xz;
        RTuv = RTuv/(_OrthographicCamSize/1.25);
        RTuv += 0.5;
        float3 rippleRT = tex2D( _RippleTex, RTuv );
        col.rgb += step(0.2,rippleRT.b) * _RainOn * 0.05;
        float4 beforeFogCol = col;

        clip(colSrc.a - _needClip);
        #if _TRUNKGROW_ON
            clip(i.center.w-_DampMinMax.w);
        #endif

        #if _LEAFGROW_ON
            clip(colSrc.a-1+_GrowProcess);
            clip(i.center.w -0.01);
        #endif
        

        #if _NEEDPREVIEW_ON
            col *= lerp(1,_previewColor,_isPreview);
        #endif
            

        UNITY_APPLY_FOG(i.fogCoord, col);
        col = lerp(col,beforeFogCol,saturate(i.worldPos.y/_FogGroundFade));

    }

    void clipFunction(in v2f i,in float4 col)
    {
        col = tex2D(_MainTex, i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw);
        clip(col.a - _needClip);

        #if _TRUNKGROW_ON
            clip(i.center.w-_DampMinMax.w);
        #endif
        #if _LEAFGROW_ON
            clip(col.a-1+_GrowProcess);
            clip(i.center.w -0.01);
        #endif
    }   

#endif