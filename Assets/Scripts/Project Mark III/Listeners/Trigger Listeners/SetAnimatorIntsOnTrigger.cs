using UnityEngine;
using System;

public class SetAnimatorIntsOnTrigger : TriggerListener
{
    [Header("Main Fields")]
    public AnimatorIntOperation[] parameterOperations;


    public override void ManagedUpdate()
    {
        if (!Listener.IsActivated)
        {
            return;
        }

        for (int i = 0; i < parameterOperations.Length; i++)
        {
            string parameter = parameterOperations[i].name;

            int origValue = GetComponent<Animator>().GetInteger(parameter);

            switch (parameterOperations[i].operation)
            {
                case AnimatorIntOperation.Operation.AddBy:
                    GetComponent<Animator>().SetInteger(parameter,
                        origValue + parameterOperations[i].operand);
                    break;

                case AnimatorIntOperation.Operation.MultiplyBy:
                    GetComponent<Animator>().SetInteger(parameter,
                        origValue * parameterOperations[i].operand);
                    break;

                case AnimatorIntOperation.Operation.DivideBy:
                    GetComponent<Animator>().SetInteger(parameter,
                        origValue / parameterOperations[i].operand);
                    break;

                case AnimatorIntOperation.Operation.Modulo:
                    GetComponent<Animator>().SetInteger(parameter,
                        origValue % parameterOperations[i].operand);
                    break;
            }
        }
    }

    [Serializable]
    public class AnimatorIntOperation
    {
        public enum Operation
        {
            AddBy,
            MultiplyBy,
            DivideBy,
            Modulo
        }

        public string name;
        public Operation operation;
        public int operand;
    }
}
