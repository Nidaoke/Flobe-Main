 Shader "Custom/Masking" {
     Properties
     {
       _Color ("Main Color", Color) = (1, 1, 1, 1)
       _MainTex ("Base (RGBA)", 2D) = "white" {}
       _Mask ("Culling Mask", 2D) = "white" {}
       _Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
      }
      
     SubShader {
         Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
         LOD 200
         
         Lighting Off
         ZWrite Off
         Blend SrcAlpha OneMinusSrcAlpha
         AlphaTest GEqual [_Cutoff]
         
         
         Pass {
           SetTexture [_Mask] {combine texture}
           SetTexture [_MainTex] {
               combine texture, previous * constant ConstantColor[_Color]
           }
           
         }
     } 
 }