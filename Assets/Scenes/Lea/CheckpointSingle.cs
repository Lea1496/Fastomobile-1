
using UnityEngine;
/// Source:https://www.youtube.com/watch?v=IOYNg6v9sfc

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Car>(out Car car))
        {
            trackCheckpoints.CarThroughCheckpoint(this, other.transform);
            //Debug.Log(other.transform.name);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
}
