/*
 *  (c) Copyright 2020 Warwick Molloy
 *  Provided under the MIT License
 */


using Unity.Burst;

using UnityEngine.Jobs;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;


[BurstCompile(CompileSynchronously = true)]
public struct NoiseSceneUpdateJob : IJobParallelForTransform
{
    [ReadOnly] public NativeArray<float3> Positions;

    public static JobHandle Begin(
        TransformAccessArray transformArray,
        NativeArray<float3> positions,
        JobHandle priorJob
    )
    {
        var job = new NoiseSceneUpdateJob()
        {
            Positions = positions
        };

        return IJobParallelForTransformExtensions.Schedule(job, transformArray, priorJob);
    }

    public void Execute(int index, TransformAccess transform)
    {
        transform.position = Positions[index];
    }
}
