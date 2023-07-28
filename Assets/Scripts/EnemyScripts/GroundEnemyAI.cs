using System;
using System.Collections;
using System.Collections.Generic;
using Interfeces;
using Triggers;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GroundedEnemy groundedEnemy;
    [SerializeField] private int rangeOfPatrol;
    [SerializeField] private int rangeOfSavingZone;
    [SerializeField] private float delayBetweenMoving;
    [SerializeField] private Animator enemyAnimator;


    [SerializeField] private GameObject savingPosition;
    [SerializeField] private GameObject goal;
    private float arrivalAtDestination;

    private float startXPos;

    internal GoalTrigger goalTrigger;
    internal SavingPositionTrigger savingPositionTrigger;
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Start()
    {
        startXPos = transform.position.x;
        goal = new GameObject("Goal");
        SphereCollider goalCollider = goal.AddComponent<SphereCollider>();
        goalCollider.isTrigger = true;
        GoalTrigger goalTrig = goal.AddComponent<GoalTrigger>();
        goalTrig.groundedEnemyAi = this;
        savingPosition = new GameObject("SavingPos");
        SphereCollider savingCollider = savingPosition.AddComponent<SphereCollider>();
        savingCollider.isTrigger = true;
        SavingPositionTrigger savingPos = savingPosition.AddComponent<SavingPositionTrigger>();
        savingPos.groundedEnemy = groundedEnemy;
        var position = transform.position;
        if (startXPos < 0)
        {
            goal.transform.position = position + new Vector3(rangeOfPatrol, 0, 0);
            savingPosition.transform.position = position + new Vector3(-rangeOfSavingZone, 0, 0);
        }
        else
        {
            goal.transform.position = position + new Vector3(-rangeOfPatrol, 0, 0);
            savingPosition.transform.position = position + new Vector3(rangeOfSavingZone, 0, 0);
        }
    }

    public GroundedEnemy GetGroundedEnemy()
    {
        return groundedEnemy;
    }

    private bool CheckStateEnemy()
    {
        switch (groundedEnemy.GetState())
        {
            case IState.State.Attack:
                return false;

            case IState.State.Damage:
                return false;

            case IState.State.Die:
                return false;

            case IState.State.Stand:
                return false;

            default:
                return true;
        }
    }

    public void Patrol()
    {
        if (CheckStateEnemy()) return;

        if (goalTrigger != null)
        {
            DelayBetweenChangeGoalPos();
        }

        var position = goal.transform.position;
        navMeshAgent.destination = position;
        enemyAnimator.SetFloat(Vertical, Vector3.Distance(position, transform.position));
    }

    private void DelayBetweenChangeGoalPos()
    {
        if (Time.time - arrivalAtDestination > delayBetweenMoving)
        {
            Debug.Log("Change goal pos");
            rangeOfPatrol *= -1;
            if (startXPos < 0)
            {
                goal.transform.position += new Vector3(rangeOfPatrol * 2, 0, 0);
            }
            else
            {
                goal.transform.position -= new Vector3(rangeOfPatrol * 2, 0, 0);
            }
            arrivalAtDestination = Time.time;
        }
    }

    public void EscapeToSalvation()
    {
        navMeshAgent.destination = savingPosition.transform.position;
    }
}