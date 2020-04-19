using BBUnity.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;

namespace BBUnity.Actions
{
    public enum MessageType
    {
        String,
        Location
    }

    [Action("DED/PrintBB")]
    public class PrintBB : GOAction
    {
        [InParam("Message Type")]
        public MessageType MessageType;

        [InParam("Message")]
        public String Message;

        [InParam("Location")]
        public Vector2 Location;

        public override TaskStatus OnUpdate()
        {
            switch (MessageType)
            {
                case MessageType.String:
                    if (Message is null)
                    {
                        Debug.LogError("Message is null");
                        return TaskStatus.FAILED;
                    }
                    break;
                case MessageType.Location:
                    Debug.Log($"{nameof(Location)}: {Location}");
                    break;
                default:
                    Debug.LogError($"Unknown type {MessageType}");
                    return TaskStatus.ABORTED;
            }
            return TaskStatus.COMPLETED;
        }
    }
}
