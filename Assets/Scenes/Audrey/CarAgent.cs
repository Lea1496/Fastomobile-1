using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

/// Source:https://www.youtube.com/watch? v=2X5m_nDBvS4&t=340s&ab_channel=CodeMonkey
/// Source:https://github.com/rNuv/self-driving-car-unity/blob/main/CarAgent.cs

public class CarAgent : Agent
{
    private TrackCheckpoints trackCheckpoints;
    Vector3 spawnPosition;
    Vector3 spawnForward;

    private Car car;

    private void Start()
    {
        trackCheckpoints = GameObject.FindObjectOfType <TrackCheckpoints>();
        //Debug.Log(trackCheckpoints);

        trackCheckpoints.OnCarCorrectCheckpoint += TrackCheckpoints_OnCarCorrectCheckpoint;
        trackCheckpoints.OnCarWrongCheckpoint += TrackCheckpoints_OnCarWrongCheckpoint;

        car = GetComponent<Car>();
        spawnPosition = gameObject.transform.position;
        spawnForward = gameObject.transform.forward;
    }

    private void TrackCheckpoints_OnCarWrongCheckpoint(object sender, TrackCheckpoints.CarCheckpointEventArgs e)
    {
        if (e.carTransform == transform)
        {
            AddReward(-1f);
            Debug.Log("Wrong");
        }
    }

    private void TrackCheckpoints_OnCarCorrectCheckpoint(object sender, TrackCheckpoints.CarCheckpointEventArgs e)
    {
        if (e.carTransform == transform)
        {
            AddReward(+1f);
            Debug.Log("Right");
        }
    }

    public override void OnEpisodeBegin()
    {
        car.StopCompletely();
        transform.position = spawnPosition /*+ new Vector3(Random.Range(-0.5f, +0.5f), 0f, Random.Range(-0.5f, +0.5f))*/;
        transform.forward = spawnForward;
        trackCheckpoints.ResetCheckpoints(transform);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(transform).transform.forward;
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float horizontalI = actions.ContinuousActions[0];
        float verticalI = actions.ContinuousActions[1];

        car.SetInputs(verticalI, horizontalI);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

        //Debug.Log(actionsOut.ContinuousActions[0]);
        //Debug.Log(actionsOut.ContinuousActions[1]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Mur>(out Mur mur))
        {
            AddReward(-0.5f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Mur>(out Mur mur))
        {
            AddReward(-0.1f);
        }
    }

    // Discrete actions :

    //public override void OnActionReceived(ActionBuffers actions)
    //{
    //    float verticalI = 0f;
    //    float horizontalI = 0f;

    //    switch (actions.DiscreteActions[0])
    //    {
    //        case 0: verticalI = 0f; break;
    //        case 1: verticalI = +1f; break;
    //        case 2: verticalI = -1f; break;
    //    }

    //    switch (actions.DiscreteActions[1])
    //    {
    //        case 0: horizontalI = 0f; break;
    //        case 1: horizontalI = +1f; break;
    //        case 2: horizontalI = -1f; break;
    //    }
    //    car.SetInputs(verticalI, horizontalI);
    //}

    //public override void Heuristic(in ActionBuffers actionsOut)
    //{
    //    int verticalAction = 0;
    //    if (Input.GetKey(KeyCode.UpArrow))
    //        verticalAction = 1;
    //    if (Input.GetKey(KeyCode.DownArrow))
    //        verticalAction = 2;

    //    int horisontalAction = 0;
    //    if (Input.GetKey(KeyCode.RightArrow))
    //        horisontalAction = 1;
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //        horisontalAction = 2;

    //    ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
    //    discreteActions[0] = verticalAction;
    //    discreteActions[1] = horisontalAction;

    //}
}
