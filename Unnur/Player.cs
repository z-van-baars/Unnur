using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Player : Character
    {
        private int maxSpeed = 11;
        public enum KeyInput
        {
            MoveLeft = 0,
            MoveRight,
            Jump,
            Count
        }
        public enum MouseInput
        {
            Attack = 0,
            Count
        }
        public enum CharacterState
        {
            Stand,
            Walk,
            Jump,
            Crouch
        }
        public bool[] KInputs;
        public bool[] PrevKInputs;
        public bool[] MInputs;
        public bool[] PrevMInputs;
        public new CharacterState CurrentState = CharacterState.Stand;
        public const float JumpSpeed = -15;
        public const float MinJumpSpeed = -5;
        public const float WalkSpeed = 5;

        public Player(Vector2 dimensions, Vector2 coordinates, Scene currentScene) : base(dimensions, coordinates, dimensions, currentScene)
        {
            coordinates = new Vector2();
            KInputs = new bool[(int)KeyInput.Count];
            PrevKInputs = new bool[(int)KeyInput.Count];
            MInputs = new bool[(int)MouseInput.Count];
            PrevMInputs = new bool[(int)MouseInput.Count];

        }
        protected bool Released(KeyInput key)
        {
            return (!KInputs[(int)key] && PrevKInputs[(int)key]);
        }

        protected bool KeyState(KeyInput key)
        {
            return (KInputs[(int)key]);
        }

        protected bool Pressed(KeyInput key)
        {
            return (KInputs[(int)key] && !PrevKInputs[(int)key]);
        }
        public void UpdatePrevInputs()
        {
            var count1 = (byte)KeyInput.Count;

            for (byte i = 0; i < count1; i++)
            {
                PrevKInputs[i] = KInputs[i];
            }
            var count2 = (byte)MouseInput.Count;
            for (byte j = 0; j < count2; j++)
            {
                PrevMInputs[j] = MInputs[j];
            }
        }
        public void KeyInputSetter(KeyboardState keyState, MouseState mouseState)
        {
            KInputs[(int)KeyInput.MoveRight] = keyState.IsKeyDown(Keys.D);
            KInputs[(int)KeyInput.MoveLeft] = keyState.IsKeyDown(Keys.A);
            KInputs[(int)KeyInput.Jump] = keyState.IsKeyDown(Keys.Space);
        }
        public override void Update(KeyboardState keyState, MouseState mouseState, Collision collision, Scene currentScene)
        {
            KeyInputSetter(keyState, mouseState);
            switch (CurrentState)
            {
                case CharacterState.Stand:
                    SetVelocity(0, 0);
                    /// some spritesheet setting logic should go here too


                    if (KeyState(KeyInput.MoveRight) != KeyState(KeyInput.MoveLeft))
                    {
                        CurrentState = CharacterState.Walk;
                        break;
                    }
                    else if (KeyState(KeyInput.Jump))
                    {
                        SetVelocity(Velocity.X, JumpSpeed);
                        CurrentState = CharacterState.Jump;
                        /// play Jump sound
                        break;
                    }
                    else if (!OnGround)
                    {
                        CurrentState = CharacterState.Jump;
                        break;
                    }
                    break;
                case CharacterState.Walk:
                    /// Walk animation stuff goes here
                    /// 
                    //if both or neither left nor right keys are pressed then stop walking and stand
                    if (KeyState(KeyInput.MoveRight) == KeyState(KeyInput.MoveLeft))
                    {
                        CurrentState = CharacterState.Stand;
                        SetVelocity(0, Velocity.Y);
                        break;
                    }
                    else if (KeyState(KeyInput.MoveRight))
                    {
                        if (PushesRightWall)
                        {
                            SetVelocity(0, Velocity.Y);
                        }
                        else
                        {
                            SetVelocity(WalkSpeed, Velocity.Y);
                        }
                    }
                    else if (KeyState(KeyInput.MoveLeft))
                    {
                        if (PushesLeftWall)
                        {
                            SetVelocity(0, Velocity.Y);
                        }
                        else
                        {
                            SetVelocity(-WalkSpeed, Velocity.Y);
                        }
                    }
                    //if there's no tile to walk on, fall
                    if (KeyState(KeyInput.Jump))
                    {
                        SetVelocity(Velocity.X, JumpSpeed);
                        CurrentState = CharacterState.Jump;
                        /// play Jump sound
                        break;
                    }

                    else if (!OnGround)
                    {
                        CurrentState = CharacterState.Jump;
                        break;
                    }
                    break;
                case CharacterState.Jump:
                    /// gravity thingus dingus
                    /// 
                    /// Animation timing stuff goes here
                    SetVelocity(Velocity.X, Math.Min(maxSpeed, Velocity.Y + 1));
                    if (!KeyState(KeyInput.Jump) && Velocity.Y > 0.0f)
                    {
                        // SetVelocity(Velocity.X, Math.Max(Velocity.Y, MinJumpSpeed));
                    }
                    if (KeyState(KeyInput.MoveRight) == KeyState(KeyInput.MoveLeft))
                    {
                        SetVelocity(0, Velocity.Y);
                    }
                    else if (KeyState(KeyInput.MoveRight))
                    {
                        if (PushesRightWall)
                            SetVelocity(0, Velocity.Y);
                        else
                            SetVelocity(WalkSpeed, Velocity.Y);
                    }
                    else if (KeyState(KeyInput.MoveLeft))
                    {
                        if (PushesLeftWall)
                            SetVelocity(0, Velocity.Y);
                        else
                            SetVelocity(-WalkSpeed, Velocity.Y);
                    }

                    //if we hit the ground
                    if (OnGround)
                    {
                        //if there's no movement change state to standing
                        if (KInputs[(int)KeyInput.MoveRight] == KInputs[(int)KeyInput.MoveLeft])
                        {
                            CurrentState = CharacterState.Stand;
                            SetVelocity(Velocity.X, 0);
                        }
                        else    //either go right or go left are pressed so we change the state to walk
                        {
                            CurrentState = CharacterState.Walk;
                            SetVelocity(Velocity.X, 0);
                        }
                    }
                    break;
                case CharacterState.Crouch:
                    break;
            }
            
            UpdatePhysics();

            if (OnGround && !WasOnGround)
            {
                /// play landing sound effect
            }
            if (!WasAtCeiling && AtCeiling
                || !PushedLeftWall && PushesLeftWall
                || !PushedRightWall && PushesRightWall)
            {
                /// play bumping sound if it's different from planding sound
            }
            OnGround = IsOnGround(collision, currentScene);
            UpdatePrevInputs();
            ResetOccupiedTiles(currentScene);
        }
    }
}
