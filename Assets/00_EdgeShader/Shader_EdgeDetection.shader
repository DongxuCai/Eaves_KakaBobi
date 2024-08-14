Shader "PostProcess/EdgeDetection"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        CGINCLUDE
        
        #include "UnityCG.cginc"
        #define LUM(c) ((c).r*.3 + (c).g*.59 + (c).b*.11)
        
        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        sampler2D _BuildTex;
        sampler2D _RenderTex01;
        sampler2D _RenderTex02;
        fixed4 _EdgeColor,_FogEdgeColor,_BuildCol;
        float _FogEdgeColorFactor,_FogEdgeWidthFactor;
        float _useObjcetColor,_fogUseObjcetColor;
        float _SampleDistance;
        half4 _Sensitivity;
        float2 _ScreenPos;
        float _ScreenWaveTime,_ScreenWaveAlpha;
        float _ScreenWaveSpeed,_ScreenWaveWidth,_ScreenWavePower,_ScreenWaveSharp;

        float _Threshold,_ColorWidth,_ColorStrength,_ChangeMode,_BuildColLerp,_RenderColLerp,_NoisePower,_NoiseFactor;
        float4 _BuildSaturation,_BuildContrast,_PureMaskRange;
        float _RoamSaturation,_RoamContrast,_Render01Size;
        float2 _NoiseRange;
        
        sampler2D _CameraDepthTexture; // 深度纹理
        sampler2D _VertexColorMask; // 顶点色控制边缘与否
        sampler2D _ScreenNoise; // 顶点色控制边缘与否
        float4 _CameraDepthTexture_TexelSize;
        
        struct v2f
        {
            float4 pos: SV_POSITION;
            half2 uv: TEXCOORD0;
        };
        
        v2f vert(appdata_img v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;

            // UNITY_UV_STARTS_AT_TOP，纹理的坐标系原点在纹理顶部的平台上值：Direct3D类似平台是1；OpenGL类似平台是0
            #if UNITY_UV_STARTS_AT_TOP
                // 在Direct3D平台下，如果我们开启了抗锯齿，则xxx_TexelSize.y 会变成负值，好让我们能够正确的进行采样。
                // 所以if (_MainTex_TexelSize.y < 0)的作用就是判断我们当前是否开启了抗锯齿。
                if (_MainTex_TexelSize.y < 0)
                o.uv.y = 1 - v.texcoord.y;
            #endif

            return o;
        }
        
        // 计算对角线上两个纹理值的差值，返回0表明这两点之间存在一条边界，反之则返回1
        half CheckSame(float3 normalCenter,float3 normalSample,half4 center, half4 sample,float DepthMask,float NormalMask)
        {

            // 获取两个采样点的法线和深度值
            // 这里并没有解码得到真正的法线值 而是直接使用了 xy 分量，是因为只需要比较两个采样值之间的差异度，
            // 而并不需要知道它们真正的法线值。
            half2 centerNormal = normalCenter.xy;
            float centerDepth = Linear01Depth(center.r);
            half2 sampleNormal = normalSample.xy;
            float sampleDepth = Linear01Depth(sample.r);
            
            // 把两个采样点的对应值相减并取绝对值，再乘以灵敏度参数，把差异值的每个分量相加再和一个阙值比较，
            // 如果它们的和小于阈值， 则返回1, 说明差异不明显，不存在一条边界；否则返回0，说明存在一条边界。
            half2 diffNormal = abs(centerNormal - sampleNormal) * _Sensitivity.x;
            int isSameNormal = step(diffNormal.x + diffNormal.y,0.1);
            float diffDepth = abs(centerDepth - sampleDepth) * _Sensitivity.y;
            int isSameDepth = step(diffDepth,0.1 * centerDepth);

            isSameNormal = saturate(isSameNormal + 1 - NormalMask);
            isSameDepth = saturate(isSameDepth + 1 - DepthMask);
            
            // 最后， 把法线和深度的检查结果相乘，作为组合后的返回值。
            //return isSameDepth;
            return isSameNormal * isSameDepth;
        }

        bool equals(float4 v1, float4 v2) {
            return (abs(v1.x - v2.x) <= 0.001) && (abs(v1.y - v2.y) <= 0.001) && (abs(v1.z - v2.z) <= 0.001) && (abs(v1.w - v2.w) <= 0.001);
        }




        float getRawDepth(float2 uv,float2 waveUV) { return SAMPLE_DEPTH_TEXTURE_LOD(_CameraDepthTexture, float4(uv+waveUV, 0.0, 0.0)); }

        float3 viewSpacePosAtScreenUV(float2 uv,float2 waveUV)
        {
            float3 viewSpaceRay = mul(unity_CameraInvProjection, float4(uv * 2.0 - 1.0, 1.0, 1.0) * _ProjectionParams.z);
            float rawDepth = getRawDepth(uv,waveUV);
            return viewSpaceRay * Linear01Depth(rawDepth);
        }
        float3 viewSpacePosAtPixelPosition(float2 vpos,float2 waveUV)
        {
            float2 uv = vpos * _CameraDepthTexture_TexelSize.xy;
            return viewSpacePosAtScreenUV(uv,waveUV);
        }
        half3 viewNormalAtPixelPosition(float2 vpos,float2 waveUV)
        {
            half3 viewSpacePos_l = viewSpacePosAtPixelPosition(vpos + float2(-1.0, 0.0)*0.5,waveUV);
            half3 viewSpacePos_r = viewSpacePosAtPixelPosition(vpos + float2( 1.0, 0.0)*0.5,waveUV);
            half3 viewSpacePos_d = viewSpacePosAtPixelPosition(vpos + float2( 0.0,-1.0)*0.5,waveUV);
            half3 viewSpacePos_u = viewSpacePosAtPixelPosition(vpos + float2( 0.0, 1.0)*0.5,waveUV);
            half3 hDeriv = (viewSpacePos_r - viewSpacePos_l);
            half3 vDeriv = (viewSpacePos_u - viewSpacePos_d);
            half3 viewNormal = normalize(cross(hDeriv, vDeriv));
            half3 WorldNormal = mul((float3x3)unity_CameraToWorld, viewNormal * half3(1.0, 1.0, -1.0));
            return half4(GammaToLinearSpace(WorldNormal.xyz * 0.5 + 0.5), 1.0);
        }

        float Remap(float In, float2 InMinMax, float2 OutMinMax)
        {
            return OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }

        float easeOutQuart(float x) 
        {
           return x < 0.5 ? 8 * x * x * x * x : 1 - pow(-2 * x + 2, 4) / 2;
        }




        fixed4 fragRobertsCrossDepthAndNormal(v2f i): SV_Target
        {
            float2 scale = float2(_ScreenParams.x / _ScreenParams.y, 1);

            //水波纹
            //屏幕长宽比 缩放因子
            float ScreenNoise = tex2D(_ScreenNoise,i.uv*scale).r;
            float2 dir = _ScreenPos/_ScreenParams.xy-i.uv;
            float dis = length(dir * scale);
            float sphere1 = smoothstep(_ScreenWaveWidth,1, 1 - saturate(dis / ((_ScreenWaveTime + 0.001) * _ScreenWaveSpeed)));
            float sphere2 = saturate(saturate(dis / ((_ScreenWaveTime + 0.001) * _ScreenWaveSpeed) - (_ScreenWaveTime+0.1))*_ScreenWaveSharp/(_ScreenWaveTime+0.1));
            float atZoomArea = saturate(sphere1*sphere2) * (ScreenNoise);
            float2 waveUV = dir * _ScreenWavePower * atZoomArea * _ScreenWaveAlpha;
            //waveUV = 0;
            i.uv += waveUV;


            float4 VertexColMask = tex2D(_VertexColorMask, i.uv);
            float DepthMask = VertexColMask.r;
            float NormalMask = VertexColMask.g;
            float ColorEdgeMask = VertexColMask.b;
            float PureColorMask = VertexColMask.a;
            
            half edge = 1.0;

            //深度
            half4 sample1 = tex2D(_CameraDepthTexture, i.uv + _MainTex_TexelSize.xy * half2(1, 1) * _SampleDistance);
            half4 sample2 = tex2D(_CameraDepthTexture, i.uv + _MainTex_TexelSize.xy * half2(-1, -1) * _SampleDistance);
            half4 sample3 = tex2D(_CameraDepthTexture, i.uv + _MainTex_TexelSize.xy * half2(-1, 1) * _SampleDistance);
            half4 sample4 = tex2D(_CameraDepthTexture, i.uv + _MainTex_TexelSize.xy * half2(1, -1) * _SampleDistance);

            //法线
            half3 viewNormal01 = viewNormalAtPixelPosition(i.pos.xy + half2(0.5, 0.5) * _SampleDistance,waveUV);
            half3 viewNormal02 = viewNormalAtPixelPosition(i.pos.xy + half2(-0.5, -0.5) * _SampleDistance,waveUV);
            half3 viewNormal03 = viewNormalAtPixelPosition(i.pos.xy + half2(-0.5, 0.5) * _SampleDistance,waveUV);
            half3 viewNormal04 = viewNormalAtPixelPosition(i.pos.xy + half2(0.5, -0.5) * _SampleDistance,waveUV);
            edge *= CheckSame(viewNormal01,viewNormal02,sample1, sample2,DepthMask,NormalMask);
            edge *= CheckSame(viewNormal03,viewNormal04,sample3, sample4,DepthMask,NormalMask);
            
            fixed4 DepthNormalEdge = edge;

            //颜色
            float4 C = tex2D(_MainTex, i.uv);
            float4 N = tex2D(_MainTex, i.uv + half2(0, _MainTex_TexelSize.y)*_ColorWidth);
            float4 S = tex2D(_MainTex, i.uv - half2(0, _MainTex_TexelSize.y)*_ColorWidth);
            float4 W = tex2D(_MainTex, i.uv - half2(_MainTex_TexelSize.x, 0)*_ColorWidth);
            float4 E = tex2D(_MainTex, i.uv + half2(_MainTex_TexelSize.x, 0)*_ColorWidth);

            float C_lum = LUM(C);
            float N_lum = LUM(N);
            float S_lum = LUM(S);
            float W_lum = LUM(W);
            float E_lum = LUM(E);
            

            float L_lum = saturate((N_lum + S_lum + W_lum + E_lum - 4 * C_lum)*_ColorStrength * 10);
            float ColorEdge = 1 - step(_Threshold,L_lum);
            ColorEdge = saturate(ColorEdge + 1 - ColorEdgeMask);

            //采样
            float depth = Linear01Depth(getRawDepth(i.uv,0));
            float noisePower = _NoisePower * (1 - saturate(depth*_NoiseFactor));
            float4 paperCol = tex2D(_BuildTex, i.uv);
            float2 paperNoise = Remap(paperCol.xy,_NoiseRange,float2(-0.01,0.01)*noisePower);
            float4 opaqueColor = tex2D(_MainTex, i.uv + paperNoise);

            //Fog影响描边颜色
            float4 fogEdgeColor = lerp(_FogEdgeColor,opaqueColor,_fogUseObjcetColor);
            float4 edgeColor = lerp(_EdgeColor,opaqueColor,_useObjcetColor);
            float4 edgeCol = lerp(edgeColor,fogEdgeColor,saturate(depth*_FogEdgeColorFactor));

            //模式切换
            float2 renderUV = i.uv*scale*_Render01Size+float2(0.5,0.5) -_ScreenPos/_ScreenParams.xy*scale*_Render01Size;
            float renderTex01 = tex2D(_RenderTex01, renderUV).r;
            float renderTex02 = tex2D(_RenderTex02, i.uv).r;
            renderTex01 = saturate(smoothstep(-0.3+_ScreenWaveTime,0+_ScreenWaveTime,renderTex01)+ (1-_ScreenWaveAlpha));
            renderTex02 = smoothstep(-0.3+_ChangeMode,0+_ChangeMode,renderTex02);

            //纯色mask
            float depthPureMask = (1 - saturate(depth*15))* PureColorMask;
            float pureMaskR =(_PureMaskRange.r*opaqueColor.r);
            float pureMaskG =(_PureMaskRange.g*opaqueColor.g);
            float pureMaskB =(_PureMaskRange.b*opaqueColor.b);
            float4 pureMask = float4(pureMaskR,pureMaskG,pureMaskB,1) * depthPureMask;
            
            //两个edge混合
            float4 finalEdge = step(0.5,DepthNormalEdge * ColorEdge);
            finalEdge = lerp(finalEdge,1,saturate(depth*_FogEdgeWidthFactor));

            //漫游 建造模式混合
            float4 roamCol = lerp(edgeCol, opaqueColor,finalEdge);
            float greyCol = Remap((opaqueColor.r+opaqueColor.g+opaqueColor.b) / 3.0,float2(0,0.6),float2(0,1));     
            float4 buildCol = lerp(opaqueColor,_BuildCol*greyCol,_BuildColLerp)*paperCol;

            //纯色部分加饱和度和对比度
            fixed gray = 0.2125 * opaqueColor.r + 0.7154 * opaqueColor.g + 0.0721 * opaqueColor.b;
            float4 SatuColor = lerp(gray, opaqueColor, _BuildSaturation);
            buildCol = lerp(buildCol,SatuColor,pureMask);
            float4 consColor = lerp(0.5,opaqueColor,_BuildContrast);
            buildCol = lerp(buildCol,consColor,pureMask);
            buildCol = lerp(edgeCol, buildCol,finalEdge);
            buildCol = lerp(buildCol,lerp(roamCol,buildCol,saturate(_RenderColLerp)),saturate(1 - renderTex01));

            roamCol = lerp(gray, opaqueColor, _RoamSaturation);
            roamCol = lerp(0.5,roamCol,_RoamContrast);
            roamCol = lerp(edgeCol, roamCol,finalEdge);

            float4 finalCol = lerp(roamCol,buildCol,renderTex02);

            return finalCol;
        }

        ENDCG

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment fragRobertsCrossDepthAndNormal

            ENDCG

        }
        
    }

    Fallback Off
}
