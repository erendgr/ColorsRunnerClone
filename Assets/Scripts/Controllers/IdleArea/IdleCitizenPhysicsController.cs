﻿using System.Collections;
using Datas;
using Datas.ValueObjects;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers.IdleArea
{
    public class IdleCitizenPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField] private GameObject collectEffect;

        #endregion

        #region Private Variables

        private IdleCitizenManager _manager;
        private IdleCitizenData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _manager = transform.parent.GetComponent<IdleCitizenManager>();
        }

        public void GetData(IdleCitizenData data)
        {
            _data = data;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                StartCoroutine(WaitOnTargetPosition(other.GetComponent<IdleCarTargetController>().GetData()));
                return;
            }

            if (other.CompareTag("Player"))
            {
                Destroy(Instantiate(collectEffect, transform.position, transform.rotation), 1f);
                StopAllCoroutines();
                _manager.CollideWithPlayer();
            }
        }

        IEnumerator WaitOnTargetPosition(IdleTargetData data)
        {
            yield return new WaitForSeconds(0.5f * (_data.ReachingTime / 5));
            _manager.SetAnimation(IdleCitizenAnimStates.Idle);

            yield return new WaitForSeconds(_data.WaitingTime);
            _manager.SelectRandomDirection(data);
        }
    }
}