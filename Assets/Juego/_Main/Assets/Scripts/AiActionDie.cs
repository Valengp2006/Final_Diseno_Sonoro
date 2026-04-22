using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
    public class AIActionDie : AIAction
    {
        protected CharacterCrouch _characterCrouch;

        /// <summary>
        /// On init we grab our CharacterRun component
        /// </summary>
        public override void Initialization()
        {

        }

        /// <summary>
        /// On PerformAction we start running
        /// </summary>
        public override void PerformAction()
        {
            gameObject.SetActive(false);
        }
    }
}

