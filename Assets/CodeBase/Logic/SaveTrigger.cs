using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider Collider;
        
        void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved");
            gameObject.SetActive(false);
        }

        void OnDrawGizmos()
        {
            if (!Collider)
            {
                return;
            }
            
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}