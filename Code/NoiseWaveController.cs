/*
 *  (c) Copyright 2020 Warwick Molloy
 *  Provided under the MIT License
 */


using UnityEngine;
using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

[AddComponentMenu("Noise Move Demo/NoiseWaveController")]
public class NoiseWaveController : MonoBehaviour
{
    [SerializeField] private SizeParams Sizes = new SizeParams() {
        NumberOfRows = 10,
        NumberPerRow = 10,
        RowHeight = 1f,
        ColumnWidth = 1f
    };

    [SerializeField] private GameObject Prefab = null;
    [SerializeField] private NoiseFuncKinds NoiseFunction = NoiseFuncKinds.CNoise;

    private GameObject[] _Instances = null;
    private TransformAccessArray _Transforms;
    private NativeArray<float3> _Positions;
    private JobHandle _MoveJob, _SceneJob;

    void Awake()
    {
        CreateInstances();
        CreateTransformArray();
        CreatePositions();
    }


    void Update()
    {
        if ( _MoveJob.IsCompleted && _SceneJob.IsCompleted)
        {
            JobHandle.CompleteAll(ref _MoveJob, ref _SceneJob);
            _MoveJob = NoisePositionJob.Begin(_Positions, Sizes, NoiseFunction);
            _SceneJob = NoiseSceneUpdateJob.Begin(_Transforms, _Positions, _MoveJob);
        }
    }

    private void OnDestroy()
    {
        JobHandle.ScheduleBatchedJobs();
        JobHandle.CompleteAll(ref _MoveJob, ref _SceneJob);
        _Transforms.Dispose();
        _Positions.Dispose();
    }

    private void CreateInstances()
    {
        if (IsReady())
        {
            _Instances = new GameObject[NumberInstances()];
            Debug.Log($"Noise Move Demo: Created {_Instances.Length} objects to move");
            for(int index = 0; index < _Instances.Length; index++)
            {
                GameObject instance = GameObject.Instantiate(
                    Prefab,
                    UtilFuncs.MakePosition(index, 0, Sizes, NoiseFunction),
                    Quaternion.identity
                );
                _Instances[index] = instance;
            }
        } else
        {
            Debug.LogWarning("Prefab setting still NULL");
        }
    }

    private void CreatePositions()
    {
        if (_Instances != null && _Instances.Length > 0)
        {
            _Positions = new NativeArray<float3>(_Instances.Length, Allocator.Persistent);
        }
    }

    private void CreateTransformArray()
    {
        if (IsReady() && _Instances != null && _Instances.Length > 0)
        {
            TransformAccessArray.Allocate(
                _Instances.Length,
                UtilFuncs.BATCH_SIZE,
                out _Transforms
            );
            
            for(int index = 0; index < _Instances.Length; index++)
            {
                _Transforms.Add(_Instances[index].GetComponent<Transform>());
            }
        }
    }

    private bool IsReady() => Prefab != null;

    private int NumberInstances() => Sizes.NumberOfRows * Sizes.NumberPerRow;
}
