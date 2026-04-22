using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
    public class AIDecisionIsDead : AIDecision
    {
        private Health health;

        public override void Initialization()
        {
            health = GetComponent<Health>();
            Debug.Log(health.CurrentHealth);
        }

        /// <summary>
        /// On Decide we look for a target
        /// </summary>
        /// <returns></returns>
        public override bool Decide()
        {
            return health.CurrentHealth != 0;
        }
    }
}