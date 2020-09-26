/*
 *  (c) Copyright 2020 Warwick Molloy
 *  Provided under the MIT License
 */

using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile(CompileSynchronously = true)]
public struct NoisePositionJob : IJobParallelFor
{
    [ReadOnly] public float Time;
    [WriteOnly] public NativeArray<float3> Positions;
    [ReadOnly] public SizeParams Sizes;
    [ReadOnly] public NoiseFuncKinds NoiseFunction;

    public const int BATCH_SIZE = 10;

    public static JobHandle Begin(
        NativeArray<float3> positions,
        SizeParams sizes,
        NoiseFuncKinds noiseFunc
    )
    {
        var job = new NoisePositionJob()
        {
            Time = UnityEngine.Time.realtimeSinceStartup,
            Positions = positions,
            Sizes = sizes,
            NoiseFunction = noiseFunc
        };

        if (sizes.NumberOfRows == 0 || sizes.NumberPerRow == 0)
        {
            Debug.LogWarning($"Sizes - {sizes}");
        }

        return IJobParallelForExtensions.Schedule(job, positions.Length, BATCH_SIZE);
    }

    public void Execute(int index)
    {
        Positions[index] = UtilFuncs.MakePosition(index, Time, Sizes, NoiseFunction);
    }
}
