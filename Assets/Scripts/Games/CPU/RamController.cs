using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    public class RamController : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;  
        [SerializeField] private Transform ramPoint;
        [SerializeField] private Transform reg4;
        
        
        public GameObject getManaLeft;
        public GameObject getManaCost;


        public void SpawnGetManaLeftData()
        {
            var manaLeft = Instantiate(getManaLeft, spawnPoint);
            MoveDataToReg(manaLeft.transform);
        }

        public void SpawnGetManaCostData()
        {
            
        }

        private void MoveDataToReg(Transform data)
        {
            data.DOMoveX(ramPoint.position.x, 1f).OnComplete(() =>
            {
                data.DOMove(reg4.transform.position, 1f).SetDelay(0.3f);
            });
        }
    }
    
}
