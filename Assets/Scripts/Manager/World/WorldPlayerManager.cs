using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldPlayerManager : MonoBehaviour
    {
        Interactable _interactable;

        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private int _jumpCount = 2;
        [SerializeField] private float _jumpSpeed = 2f;

        private void Update()
        {
            if (_interactable != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _interactable.EntryPoint.position, _moveSpeed * Time.deltaTime);
                if (transform.position == _interactable.EntryPoint.position)
                {
                    OnEntryPointReachedCallback();
                }
            }
        }

        public void SetDestination(Interactable interactable)
        {
            _interactable = interactable;
        }

        private void OnEntryPointReachedCallback()
        {
            _interactable.Interact();
            _interactable = null;
            StartCoroutine(TestCompletedPath());
        }

        private IEnumerator TestCompletedPath()
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + Vector3.up;
            for (int i = 0; i < _jumpCount; i++)
            {
                while (transform.position != endPos)
                {
                    transform.position = Vector3.MoveTowards(transform.position, endPos, _jumpSpeed * 2f * Time.deltaTime);
                    yield return null;
                }
                while (transform.position != startPos)
                {
                    transform.position = Vector3.MoveTowards(transform.position, startPos, _jumpSpeed * Time.deltaTime);
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}