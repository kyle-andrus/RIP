﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class MovePlayer : ScriptableObject, IPlayerCommand
    {
        private float HorizontalDir;
        private ADSRManager ADSR;
        public void ButtonDown(GameObject gameObject)
        {
            ADSR = gameObject.GetComponent<ADSRManager>();
            var Movement = ADSR.GetDirection();
            HorizontalDir = Input.GetAxis("Horizontal");

            if (ADSRManager.Direction.None == Movement)
            {
                ADSR.ResetTimers();
                ADSR.SetPhase(ADSRManager.Phase.Attack);
                ADSR.SetInputDirection(HorizontalDir);

                // Sets direction to know that player is moving.
                ADSR.SetDirection(ADSRManager.Direction.Horizontal);
            }
            
        }
        public void ButtonHold(GameObject gameObject)
        {
            ADSR = gameObject.GetComponent<ADSRManager>();
            HorizontalDir = Input.GetAxis("Horizontal");

            ADSR.SetInputDirection(HorizontalDir);
        }
        public void ButtonUp(GameObject gameObject)
        {
            bool Grounded = ADSR.GetGrounded();
            ADSR = gameObject.GetComponent<ADSRManager>();
            HorizontalDir = Input.GetAxis("Horizontal");

            ADSR.SetInputDirection(HorizontalDir);
            if (!Grounded)
            {
                // The player is mid-air so the envelope remains at Sustain.
                // Changing it release here could result in the player
                // completing the curve and dropping straight down because there
                // would be no more change in the x direction.
                ADSR.SetPhase(ADSRManager.Phase.Sustain);
            }
            else
            {
                // If it is grounded, then releasing the button means the plaer
                // is coming to a stop.
                ADSR.SetPhase(ADSRManager.Phase.Release);
                ADSR.SetDirection(ADSRManager.Direction.None);
            }
            
        }

    }
    
}