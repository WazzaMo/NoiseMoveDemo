/*
 *  (c) Copyright 2020 Warwick Molloy
 *  Provided under the MIT License
 */


using Unity.Mathematics;
using Unity.Burst;


public static class UtilFuncs
{
    public const int BATCH_SIZE = 10;

    [BurstCompile(CompileSynchronously = true)]
    public static float3 MakePosition(
        int index,
        float time,
        SizeParams sizes,
        NoiseFuncKinds noiseFunc
    )
    {
        int row = index / sizes.NumberPerRow;
        int col = (index - (row*sizes.NumberPerRow) ) % sizes.NumberPerRow;

        float2 xyPos = new float2(col * sizes.ColumnWidth, row * sizes.RowHeight);
        float height = NOISE_FUNCTION(xyPos, time, noiseFunc);
        return new float3(xyPos.x, height, xyPos.y);
    }

    [BurstCompile(CompileSynchronously = true)]
    private static float NOISE_FUNCTION(float2 xyPos, float time, NoiseFuncKinds func)
    {
        switch (func)
        {
            case NoiseFuncKinds.CNoise: return CNoise(xyPos, time);
            //case NoiseFuncKinds.PNoise: return PNoise(xyPos, time);
            case NoiseFuncKinds.PsrdNoise: return PsrdNoise(xyPos, time);
            case NoiseFuncKinds.PsrNoise: return PsrNoise(xyPos, time);
            case NoiseFuncKinds.SNoise: return SNoise(xyPos, time);
            case NoiseFuncKinds.SrdNoise: return SrdNoise(xyPos, time);
            default:
                return CNoise(xyPos, time);
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    private static float CNoise(float2 pos, float time)
        => noise.cnoise(new float3(pos.x, pos.y, time));

    [BurstCompile(CompileSynchronously = true)]
    private static float PNoise(float2 pos, float time)
        => noise.pnoise(pos, new float2(time, 0));

    [BurstCompile(CompileSynchronously = true)]
    private static float PsrdNoise(float2 pos, float time)
        => noise.psrdnoise(pos, new float2(time, time)).z;

    [BurstCompile(CompileSynchronously = true)]
    private static float SNoise(float2 pos, float time)
    => noise.snoise(new float3(pos.x, pos.y, time));

    [BurstCompile(CompileSynchronously = true)]
    private static float SrdNoise(float2 pos, float time)
        => noise.srdnoise(pos, time).z;

    [BurstCompile(CompileSynchronously = true)]
    private static float PsrNoise(float2 pos, float time)
        => noise.psrnoise(pos, new float2(time, time));
}
