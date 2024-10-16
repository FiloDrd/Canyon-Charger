﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MalbersAnimations.Scriptables;

namespace MalbersAnimations.Controller
{
    public class JumpBasic : State
    {
        public readonly static int JumpEndHash = Animator.StringToHash("JumpEnd");
        public readonly static int JumpStartHash = Animator.StringToHash("JumpStart");
        public readonly static int DoubleJumpHash = Animator.StringToHash("DoubleJump");


        [Header("Jump Parameters")]

        [Tooltip("Wait for the Animation to Activate the Jump Logic\n Use [void ActivateJump()] on the Animator")]
        public bool WaitForAnimation;

        [Tooltip("Amount of jumps the Animal can do (Double and Triple Jumps)\n**IMPORTANT**\nWhen using multiple Jumps,Fall cannot be on the Sleep From State List")]
        public IntReference Jumps = new IntReference(1);

        /// <summary>If the Jump input is pressed, the Animal will keep going Up while the Jump Animation is Playing</summary>
        [Space, Tooltip("If the Jump input is pressed, the Animal will keep going Up while the Jump Animation is Playing")]
        public BoolReference JumpPressed = new BoolReference(false);
        [Tooltip("Can the Animal be Rotated while Jumping?")]
        public BoolReference AirControl = new BoolReference(true);
        [Tooltip("Smooth Value for Changing Speed Movement on the Air")]
        public FloatReference AirSmooth = new FloatReference(5);
        [Tooltip("How much Rotation the Animal can do while Jumping")]
        public FloatReference AirRotation = new FloatReference(10);
        [Tooltip("How much Movement the Animal can do while Jumping")]
        public FloatReference AirMovement = new FloatReference(5);

        [Tooltip("How much Movement the Animal can do while Jumping")]
        public List<StateID> ResetJump;

        [Space]
        public List<JumpBasicProfile> profiles = new List<JumpBasicProfile>();


        private JumpBasicProfile activeJump;
        protected MSpeed JumpSpeed;
        private bool ActivateJumpLogic;
        private int JumpsPerformanced = 0;

        private int GravityTime = 0;
        
        private bool CanJumpAgain = false;
        private float StartedJumpLogicTime;
        private float JumpPressHeight_Value = 1;

        /// <summary> Set the values you need to Reset on the State</summary>
        public override void ResetStateValues()
        {
            CanJumpAgain = true;
            JumpPressHeight_Value = 1;
            animal.UpdateDirectionSpeed = true; //Reset the Rotate Direction
            ActivateJumpLogic = false;
            StartedJumpLogicTime = 0;
            GravityTime = 10;
        }

        //Do not use the Try Activate
        public override bool TryActivate() => false;
        

        public override void StatebyInput()
        {
            if (InputValue && (JumpsPerformanced < Jumps) && CanJumpAgain && !InCoreAnimation)  
            Activate();
        }


        public void ActivateJump()
        {
            ActivateJumpLogic = true;
            animal.Grounded = false;
            StartedJumpLogicTime = Time.time;
            GravityTime = 10;
            Debugging("[Basic Jump] Activate JumpLogic");
        }

        public override void Activate()
        {
            base.Activate();
            SetStatus(JumpsPerformanced); //UnCommon SET THE STATE STATUS ON THE ACTIVE INSTEAD OF THE EXIT
            JumpsPerformanced++;
            CanJumpAgain = false;
            General.Gravity = false;
            IsPersistent = true;                        //IMPORTANT!!!!! DO NOT ELIMINATE!!!!! 
            animal.currentSpeedModifier.animator = 1;
            animal.ResetGravityValues();                //Reset the Gravity

            activeJump = (profiles != null && profiles.Count > 0) ? profiles[0] : new JumpBasicProfile(0);
           
            foreach (var jump in profiles)                          //Save/Search the Current Jump Profile by the Lowest Speed available
            {
                if (jump.VerticalSpeed <= animal.VerticalSmooth && jump.VerticalSpeed > activeJump.VerticalSpeed)
                {
                    activeJump = jump;
                }
            }
        }


        public override void EnterCoreAnimation()
        {
            Debugging("EnterCoreAnim");
            JumpPressHeight_Value = 1;

            JumpSpeed = new MSpeed(animal.CurrentSpeedModifier) //Inherit the Vertical and the Lerps
            {
                name = "Jump Basic Speed",
                position = General.RootMotion ? 0 : animal.HorizontalSpeed, //Inherit the Horizontal Speed you have from the last state
                animator = 1,
                Vertical = animal.CurrentSpeedModifier.Vertical,
                lerpPosition = AirSmooth,
                rotation = AirRotation,
                strafeSpeed = General.RootMotion ? 0 : animal.HorizontalSpeed,
                lerpStrafe = AirSmooth
            };

            animal.UpdateDirectionSpeed = AirControl; 

            if (animal.HasExternalForce)
                animal.DirectionalSpeed = Vector3.ProjectOnPlane(animal.ExternalForce, animal.UpVector).normalized;

            animal.SetCustomSpeed(JumpSpeed, true);       //Set the Current Speed to the Jump Speed Modifier

            if (!WaitForAnimation) ActivateJump();        //if it does not require to Wait for the Animator to call

            if (animal.LastState.ID > StateEnum.Locomotion) ActivateJump(); //Mean is doing a double jump!
        }


        public override void EnterTagAnimation()
        {
            if (CurrentAnimTag == JumpStartHash && !animal.RootMotion)
            {
                var JumpStartSpeed = new MSpeed(animal.CurrentSpeedModifier)
                {
                    name = "JumpStartSpeed",
                    position = animal.HorizontalSpeed,
                    Vertical = animal.CurrentSpeedModifier.Vertical,
                    animator = 1,
                    rotation = AirControl.Value ? (!animal.UseCameraInput ? AirRotation.Value : AirRotation.Value / 10f) : 0f,
                    strafeSpeed = animal.HorizontalSpeed,
                    lerpStrafe = AirSmooth
                };

                Debugging("[EnterTag-JumpStart]");
                animal.SetCustomSpeed(JumpStartSpeed, true);       //Set the Current Speed to the Jump Speed Modifier

                if (animal.TerrainSlope > 0)   animal.UseCustomAlign = true; //Means we are jumping uphill
                  
            }
            else if (CurrentAnimTag == JumpEndHash)
            {
                Debugging("[EnterTag-JumpEnd]");
                AllowExit();
            }
        }


        public override void OnStateMove(float deltaTime)
        {
            if (InCoreAnimation && ActivateJumpLogic)
            {
                if (JumpPressed.Value)
                {
                    JumpPressHeight_Value = Mathf.Lerp(JumpPressHeight_Value, InputValue ? 1 : 0, deltaTime * AirSmooth);
                }

                Vector3 ExtraJumpHeight = (animal.UpVector * activeJump.Height.Value);
                animal.AdditivePosition += ExtraJumpHeight * deltaTime * JumpPressHeight_Value; //Up Movement


                if (AirMovement > CurrentSpeedPos && AirControl)
                    CurrentSpeedPos = Mathf.Lerp(CurrentSpeedPos, AirMovement, deltaTime * AirSmooth);

                //Apply Fake Gravity (HAD TO TO IT)

                var GTime = deltaTime * GravityTime;
                var GravityStoredVelocity = animal.Gravity * animal.GravityPower * (GTime * GTime / 2);
                animal.AdditivePosition += GravityStoredVelocity * deltaTime * activeJump.GravityPower.Value;                                         //Add Gravity if is in use
                GravityTime++;

            }
        }

        public override void TryExitState(float deltaTime)
        {
            if (JumpPressed.Value && !InputValue) AllowExit();

            else if (MTools.ElapsedTime(StartedJumpLogicTime,activeJump.JumpTime) && ActivateJumpLogic)
            {
                AllowExit();
                CanJumpAgain = true;
                animal.State_Activate(StateEnum.Fall); //Seems Important
                GravityTime = 0;
                Debugging("[Allow Exit]");
            }
        }

        /// <summary>Is called when a new State enters</summary>
        public override void NewActiveState(StateID newState)
        {
            //Reset all the jumps 
            if (newState <= StateEnum.Locomotion || ResetJump.Contains(newState))
            {
                JumpsPerformanced = 0;          //Reset the amount of jumps performanced
                CanJumpAgain = true;
            }
            else if (newState == StateEnum.Fall && animal.LastState.ID != ID) //If we were not jumping then increase the Double Jump factor
            {
                JumpsPerformanced++; //If we are in fall animation then increase a Jump perfomanced
            }
        }


#if UNITY_EDITOR
        internal void Reset()
        {
            ID = MTools.GetInstance<StateID>("Jump");
            Input = "Jump";

            SleepFromState = new List<StateID>() { MTools.GetInstance<StateID>("Fall"), MTools.GetInstance<StateID>("Fly") };
            SleepFromMode = new List<ModeID>() { MTools.GetInstance<ModeID>("Action"), MTools.GetInstance<ModeID>("Attack1") };


            General = new AnimalModifier()
            {
                RootMotion = false,
                Grounded = false,
                Sprint = false,
                OrientToGround = false,
                CustomRotation = false,
                IgnoreLowerStates = true, //IMPORTANT!
                Persistent = true,
                AdditivePosition = true,
                AdditiveRotation = true,
                Gravity = false,
                modify = (modifier)(-1),
            };

            ExitFrame = false;

            profiles = new List<JumpBasicProfile>(1) { new JumpBasicProfile() { Height = new FloatReference(5f), JumpTime = 0.33f, name = "Default", VerticalSpeed = 0 } };
        }
#endif
    }


    /// <summary>Different Jump parameters on different speeds</summary>
    [System.Serializable]
    public struct JumpBasicProfile
    {
        /// <summary>Name to identify the Jump Profile</summary>
        public string name;

        /// <summary>Maximum Vertical Speed to Activate this Jump</summary>
        [Tooltip("Maximum Vertical Speed to Activate this Profile")]
        public float VerticalSpeed;

        /// <summary>Maximum distance to land on a Cliff </summary>
        [Tooltip("Duration of the Jump")]
        public float JumpTime;

        [Tooltip("How High the animal can Jump")]
        /// <summary>Height multiplier to increase/decrease the Height Jump</summary>
        public FloatReference Height;


        [Tooltip("Multiplier for the Gravity")]
        public FloatReference GravityPower;

        public JumpBasicProfile(int Vertical)
        {
            Height = new FloatReference(6);
            GravityPower = new FloatReference(1);
            JumpTime = 0.3f;
            VerticalSpeed = Vertical;
            name = "Default";
        }
    }
}
