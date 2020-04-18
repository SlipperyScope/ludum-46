using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using System;
using BBUnity.Actions;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("DED/HelloWorld")]
    [Help("Prints a message")]
    public class HelloWorld : GOAction
    {
        [InParam("Message")]
        [Help("Message to print")]
        public String Message { get; set; }

        public override TaskStatus OnUpdate()
        {
            if (Message is null)
            {
                Debug.LogError($"{nameof(Message)} is null");
                return TaskStatus.FAILED;
            }

            Debug.Log(Message);
            return TaskStatus.COMPLETED;
        }
    }
}
