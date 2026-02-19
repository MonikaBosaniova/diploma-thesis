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

            draggable.DragEnd += () => SnapDataToReg(reg);
        }

        private void SnapDataToReg(RegData data)
        {
            data.transform.position = new Vector3(data._regParent.position.x, data.transform.position.y, data._regParent.position.z);
            
            if(data._regParent.childCount > 0 && data._regParent.GetChild(0) != data.transform) Destroy(data._regParent.GetChild(0).gameObject);
            data.transform.parent = data._regParent;
            RegTrigger regTrigger = data._regParent.GetComponent<RegTrigger>();
            regTrigger.snappedData = data;
            regTrigger.snap?.Invoke();
            regTrigger.SetHighlight(false);
        }
    }
    
}
