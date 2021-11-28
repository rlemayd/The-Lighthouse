using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DecisionTree;

public class MonsterMove : MonoBehaviour
{
    public float speed=0.5f;
    public float changeDirectionDistance = 0.1f;
    private Vector3 directionVector = Vector3.zero;
    public bool movingTowardsFinalNode;
    private ObjectDecision Root;
    [SerializeField]private GameObject initialPosition;
    [SerializeField]private GameObject finalPosition;

    void Start()
    {
        transform.position = initialPosition.transform.position;
        directionVector = (finalPosition.transform.position - transform.position).normalized;
        movingTowardsFinalNode = true;
        SetUp();
    }

    void Update()
    {
        ActionNode Result = (ActionNode)Root.Decide();
        switch (Result.Name)
        {
            case "start":
                directionVector = (initialPosition.transform.position - transform.position).normalized;
                movingTowardsFinalNode = false;
                break;
            case "end":
                directionVector = (finalPosition.transform.position - transform.position).normalized;
                movingTowardsFinalNode = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(directionVector * speed * Time.deltaTime);
    }

    private void SetUp()
    {
        //Desicions
        ObjectDecision isMovingToEndNode = new ObjectDecision(new IsMovingToEndNode(this));
        ObjectDecision hasReachedStartPoint = new ObjectDecision(new HasReachedStartLimit(this, initialPosition.transform.position));
        ObjectDecision hasReachedEndPoint = new ObjectDecision(new HasReachedEndLimit(this, finalPosition.transform.position));

        // Actions
        ActionNode goTowardsStartNode = new ActionNode("start");
        ActionNode goTowardsEndNode = new ActionNode("end");

        //Assemble the tree
        isMovingToEndNode.YesNode = hasReachedEndPoint;
        isMovingToEndNode.NoNode = hasReachedStartPoint;
        hasReachedEndPoint.YesNode = goTowardsStartNode;
        hasReachedEndPoint.NoNode = goTowardsEndNode;
        hasReachedStartPoint.YesNode = goTowardsEndNode;
        hasReachedStartPoint.NoNode = goTowardsStartNode;

        // Store the reference to the root
        Root = isMovingToEndNode;
    }
}
