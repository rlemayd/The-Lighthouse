using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
{
    public abstract class Node
    {
        public abstract Node Decide();
    }

    public abstract class DecisionNode: Node
    {
        public abstract Node GetBranch();

        public override Node Decide()
        {
            return GetBranch().Decide();
        }
    }

    public class ActionNode: Node
    {
        public string Name;

        public ActionNode(string name)
        {
            Name = name;
        }

        public override Node Decide()
        {
            return this;
        }
    }

    public abstract class BinaryDecisionNode: DecisionNode
    {
        public Node YesNode;
        public Node NoNode;

        public abstract bool Evaluate();

        public override Node GetBranch()
        {
            if (Evaluate())
                return YesNode;
            else
                return NoNode;
        }
    }

    public class ObjectDecision: BinaryDecisionNode
    {
        Evaluator evaluator;

        public ObjectDecision(Evaluator eval)
        {
            evaluator = eval;
        }

        public override bool Evaluate()
        {
            return evaluator.Evaluate();
        }
    }

    public abstract class Evaluator
    {
        public abstract bool Evaluate();
    }

    public class HasReachedStartLimit : Evaluator
    {
        Vector3 initialPos;
        MonsterMove monster;
        public float changeDirectionDistance = 0.1f;

        public HasReachedStartLimit(MonsterMove monster, Vector3 initialPosition)
        {
            this.monster = monster;
            initialPos = initialPosition;
        }

        public override bool Evaluate()
        {
            if (Vector3.Distance(monster.transform.position, initialPos) < changeDirectionDistance)
                return true;
            return false;
        }
    }

    public class HasReachedEndLimit : Evaluator
    {
        Vector3 finalPos;
        MonsterMove monster;
        public float changeDirectionDistance = 0.1f;

        public HasReachedEndLimit(MonsterMove monster, Vector3 finalPosition)
        {
            this.monster = monster;
            finalPos = finalPosition;
        }

        public override bool Evaluate()
        {
            if (Vector3.Distance(monster.transform.position, finalPos) < changeDirectionDistance)
                return true;
            return false;
        }
    }

    public class IsMovingToEndNode : Evaluator
    {
        MonsterMove monster;
        public IsMovingToEndNode(MonsterMove monster)
        {
            this.monster = monster;
        }

        public override bool Evaluate()
        {
            return monster.movingTowardsFinalNode;
        }
    }
}
