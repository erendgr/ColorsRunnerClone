using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Controllers.RunnerArea
{
    public class TurretController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private float rotationSpeed = 1f, rotationAngle = 60;

        [SerializeField] private Transform turret1;
        [SerializeField] private Transform turret2;

        [SerializeField] private ParticleSystem turret1Particle;
        [SerializeField] private ParticleSystem turret2Particle;

        #endregion

        #endregion

        private void Start()
        {
            StartCoroutine(SearchAnim());
        }

        public void RotateToPlayer(Transform player)
        {
            StopAllCoroutines();
            var position = player.position;
            turret1.DOLookAt(position, rotationSpeed);
            turret2.DOLookAt(position, rotationSpeed);
            turret1Particle.Play();
            turret2Particle.Play();
        }

        public void OnTargetDisappear()
        {
            StartCoroutine(SearchAnim());
        }

        public IEnumerator SearchAnim()
        {
            yield return new WaitForSeconds(1f);
            rotationAngle *= -1;
            turret1.DORotate(new Vector3(0, rotationAngle, 0), 1f).SetEase(Ease.InOutBack);
            turret2.DORotate(new Vector3(0, rotationAngle * -1, 0), 1f).SetEase(Ease.InOutBack);
            StartCoroutine(SearchAnim());
        }
    }
}