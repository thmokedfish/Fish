#if !defined(FLOW_INCLUDED)
#define FLOW_INCLUDED

float3 FlowUVW(float2 uv, float2 flowVector,float time,bool flowB,float2 jump,float tiling,float flowOffset) 
{
	float phaseOffset = flowB ? 0.5 : 0;
	float3 uvw;
	float progress = frac(time+phaseOffset);
	uvw.xy = uv - flowVector * (progress+flowOffset);
	uvw.xy *= tiling;
	uvw.xy += phaseOffset;
	uvw.xy += (time - progress) * jump;
	uvw.z = 1-abs(1-2*progress);
	return uvw;
}
float2 DirectionalUV(float2 uv, float3 flowVectorAndSpeed,  float tiling,float time,out float2x2 rotation)
{
	float2 dir = normalize(flowVectorAndSpeed.xy);
	rotation = float2x2(dir.y, dir.x, -dir.x, dir.y);
	uv = mul(float2x2(dir.y,-dir.x, dir.x, dir.y), uv);
	uv.y -= time * flowVectorAndSpeed.x;
	return uv * tiling;
}
#endif
