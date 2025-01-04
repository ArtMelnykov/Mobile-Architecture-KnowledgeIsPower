using System.Collections;
using CodeBase.Hero;
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

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider other)
        {
            if (_hasAggroTarget)
                return;   
            
            StopAggroCoroutine();
            SwitchFollowOn();
        }

        private void TriggerExit(Collider other)
        {
            if (!_hasAggroTarget)
                return;

            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            
            SwitchFollowOff();
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine == null)
                return;
            
            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null;
        }

        private void SwitchFollowOn()
        {
            _hasAggroTarget = true;
            _follow.enabled = true;
        }

        private void SwitchFollowOff()
        {
            _hasAggroTarget = false;
            _follow.enabled = false;
        }
    }
}