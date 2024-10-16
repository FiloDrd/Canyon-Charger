﻿using MalbersAnimations.Utilities;
using UnityEngine;


namespace MalbersAnimations.Controller.AI
{
    [CreateAssetMenu(menuName = "Malbers Animations/Pluggable AI/Tasks/Message Task", fileName = "new Message Task")]
    public class MessageTask : MTask
    {
        [Space, Tooltip("Apply the Task to the Animal(Self) or the Target(Target)")]
        public Affected affect = Affected.Self;
        [Tooltip("When you want to send the Message")]
        public ExecuteTask when = ExecuteTask.OnStart;
        public bool UseSendMessage = false;
        [Hide("ShowUpdate",true,false)]
        public float interval = 1f;
        [Tooltip("The message will be send to the Root of the Hierarchy")]
        public bool SendToRoot = true;
        public MesssageItem[] messages;                                     //Store messages to send it when Enter the animation State


        public override void StartTask(MAnimalBrain brain, int index)
        {
            if (when == ExecuteTask.OnStart)
            {
                Execute_Task(brain);
                brain.TaskDone(index); //Set Done to this task
            }
        }

        public override void UpdateTask(MAnimalBrain brain, int index)
        {
            if (when == ExecuteTask.OnUpdate)
            {
                if (MTools.ElapsedTime(brain.TasksTime[index], interval))
                {
                    Execute_Task(brain);
                    brain.SetElapsedTaskTime(index); 
                }
            }
        }

        public override void ExitAIState(MAnimalBrain brain, int index)
        {
            if (when == ExecuteTask.OnExit)
            {
                Execute_Task(brain);
            }
            brain.TaskDone(index); //Set Done to this task
        }

        private void Execute_Task(MAnimalBrain brain)
        {
            if (affect == Affected.Self)
            {
                SendMessage(SendToRoot ?  brain.transform.root : brain.transform);
            }
            else
            {
                if (brain.Target != null) SendMessage(SendToRoot ? brain.Target.root : brain.Target);
            }
        }

        public virtual void SendMessage(Transform t)
        {
            var listeners = t.root.GetComponentsInChildren<IAnimatorListener>();

            foreach (var msg in messages)
            {
                if (msg.Active && !string.IsNullOrEmpty(msg.message))
                {
                    if (UseSendMessage)
                        Messages.DeliverMessage(msg, t.transform);
                    else
                        foreach (var item in listeners) Messages.DeliverListener(msg, item);
                }
            }
        }


        void Reset()
        { Description = "Send messages to the Root game Object of the Target or the Animal using the Brain"; }

       [HideInInspector] public bool ShowUpdate;
        private void OnValidate()
        {
            ShowUpdate = when == ExecuteTask.OnUpdate;
        }
    }
}