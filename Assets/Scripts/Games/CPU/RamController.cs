using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    public class RamController : MonoBehaviour
    {
        [SerializeField] private CPULevelController cpuLevelController;
        [SerializeField] private Transform spawnPoint;  
        [SerializeField] private Transform ramPoint;
        [SerializeField] private Transform reg4;
        
        
        public GameObject getManaLeft;
        public GameObject getManaCost;


        public void SpawnGetManaLeftData()
        {
            var manaLeft = Instantiate(getManaLeft, spawnPoint);
            MoveDataToReg(manaLeft.transform, RegDataType.ManaLeft);
        }

        public void SpawnGetManaCostData()
        {
            var manaCost = Instantiate(getManaCost, spawnPoint);
            MoveDataToReg(manaCost.transform, RegDataType.Cost);
        }

        private void MoveDataToReg(Transform data, RegDataType type)
        {
            RegData reg = data.gameObject.GetComponent<RegData>();
            reg.type = type;
            
            DraggableObject draggable = data.gameObject.GetComponent<DraggableObject>();
            
            data.DOMoveX(ramPoint.position.x, 1.5f).OnComplete(() =>
            {
                data.SetLocalPositionAndRotation(new Vector3(data.localPosition.x, 0.25f, data.localPosition.z), data.rotation);
                data.DOMove(new Vector3(reg4.transform.position.x, 0.25f, reg4.transform.position.z) , 1f).SetDelay(0.3f).OnComplete(() =>
                {
                    if(reg4.childCount > 0) Destroy(reg4.GetChild(0).gameObject);
                    data.parent = reg4;
                    
                    reg._regParent = reg4;
                    draggable.draggingEnabled = true;
                });
            });
            
            RegTrigger rt = reg4.GetComponent<RegTrigger>();
            draggable.DragEnd += () => rt.SnapDataToReg(reg);
        }

        public void DespawnWizardShield()
        {
            var data = reg4.transform.GetChild(0).transform;
            data.DOMoveX(ramPoint.position.x, 1f).OnComplete(() =>
            {
                data.SetLocalPositionAndRotation(new Vector3(data.localPosition.x, 0.25f, data.localPosition.z), data.rotation);
                data.DOMove(new Vector3(spawnPoint.position.x, 0.25f, spawnPoint.position.z) , 1.5f).SetDelay(0.3f).OnComplete(() =>
                {
                    cpuLevelController.CheckFinishState();
                });
            });
        }
    }
    
}
