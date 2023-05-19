using System;
using System.Collections.Generic;
using UnityEngine;

/// Source:https://www.youtube.com/watch?v=IOYNg6v9sfc
/// Source:https://github.com/rNuv/Self-Driving-Car-Unity

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler<CarCheckpointEventArgs> OnCarCorrectCheckpoint;
    public event EventHandler<CarCheckpointEventArgs> OnCarWrongCheckpoint;

    private List<Transform> carTransformList;

    private List<int> nextCheckpointIndexList;
    private List<CheckpointSingle> checkpointSingleList;
 

    public class CarCheckpointEventArgs : EventArgs
    {
        public Transform carTransform;
    }

    private void Start()
    {
        GameObject[] checkpointsGameobjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointSingleList = new List<CheckpointSingle>();
        List<Transform> checkpointsTransform = new List<Transform>();

        for (int i = 0; i < checkpointsGameobjects.Length; i++)
        {
            checkpointsTransform.Add(checkpointsGameobjects[i].transform);
        }
        checkpointsTransform.Reverse();

        for (int i = 0; i < checkpointsTransform.Count; i++)
        {
            CheckpointSingle checkpointSingle = checkpointsTransform[i].gameObject.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }

        GameObject[] carsGameobjects = GameObject.FindGameObjectsWithTag("Player");
        carTransformList = new List<Transform>();
        for (int i = 0; i < carsGameobjects.Length; i++)
        {
            carTransformList.Add(carsGameobjects[i].transform);
        }

        nextCheckpointIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointIndexList.Add(0);
        }
    }

    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointIndex)
        {
            nextCheckpointIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointIndex + 1) % checkpointSingleList.Count;
            OnCarCorrectCheckpoint?.Invoke(this, new CarCheckpointEventArgs { carTransform = carTransform });
        }
        else
        {
            OnCarWrongCheckpoint?.Invoke(this, new CarCheckpointEventArgs { carTransform = carTransform });
        }
    }

    public void ResetCheckpoints(Transform carTransform)
    {
        nextCheckpointIndexList[carTransformList.IndexOf(carTransform)] = 0;
    }

    public CheckpointSingle GetNextCheckpoint(Transform transform)
    {
        return checkpointSingleList[nextCheckpointIndexList[carTransformList.IndexOf(transform)]];
    }
}
