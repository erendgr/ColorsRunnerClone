using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers.IdleArea
{
    public class IdleAreaMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private IdleAreaManager manager;
        [SerializeField] private List<MeshRenderer> meshes;

        #endregion

        #endregion

        public void ChangeBuildingGradient(float gra)
        {
            foreach (var mesh in meshes)
            {
                mesh.material = mesh.material;
                mesh.material.DOFloat(gra, "_Saturation", 1);
            }
        }
    }
}