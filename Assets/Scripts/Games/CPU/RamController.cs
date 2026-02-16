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
            var manaCost = Instantiate(getManaCost, spawnPoint);
            MoveDataToReg(manaCost.transform);
        }

        private void MoveDataToReg(Transform data)
        {
            data.DOMoveX(ramPoint.position.x, 1.5f).OnComplete(() =>
            {
                data.SetLocalPositionAndRotation(new Vector3(data.localPosition.x, 0.25f, data.localPosition.z), data.rotation);
                data.DOMove(new Vector3(reg4.transform.position.x, 0.25f, reg4.transform.position.z) , 1f).SetDelay(0.3f).OnComplete(() =>
                {
                    if(reg4.childCount > 0) Destroy(reg4.GetChild(0).gameObject);
                    data.parent = reg4;
                    data.gameObject.GetComponent<DraggableObject>().draggingEnabled = true;
                });
            });
        }
    }
    
}
