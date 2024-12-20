using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField]
        private TriggerObserver _triggerObserver;
        [SerializeField] 
        private AgentMoveToPlayer _follow;

        [SerializeField] 
        private float _cooldown;
        private Coroutine _aggroCoroutine;
        
        [SerializeField]
        private bool _hasAggroTarget;
            
        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            
            SwitchFollowOff();
        }

        private void TriggerEnter(Collider other)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                SwitchFollowOn();

                StartCoroutine(SwitchFollowOffAfterCooldown());
            }    
        }

        private void TriggerExit(Collider other)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            
            SwitchFollowOff();
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void SwitchFollowOn() => 
            _follow.enabled = true;

        private void SwitchFollowOff() => 
            _follow.enabled = false;
    }
}