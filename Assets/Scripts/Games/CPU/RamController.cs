using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Controls RAM data operations in the CPU minigame, spawns and despawns data objects with animations
    /// </summary>
    public class RamController : MonoBehaviour
    {
        [SerializeField] private CPULevelController cpuLevelController;
        [SerializeField] private Transform spawnPoint;  
        [SerializeField] private Transform ramPoint;
        [SerializeField] private Transform reg4;
        
        public GameObject getManaLeft;
        public GameObject getManaCost;

        /// <summary>
        /// Spawns the ManaLeft data object and moves it to the register
        /// </summary>
        public void SpawnGetManaLeftData()
        {
            var manaLeft = Instantiate(getManaLeft, spawnPoint);
            MoveDataToReg(manaLeft.transform, RegDataType.ManaLeft);
        }

        /// <summary>
        /// Spawns the ManaCost data object and moves it to the register
        /// </summary>
        public void SpawnGetManaCostData()
        {
            var manaCost = Instantiate(getManaCost, spawnPoint);
            MoveDataToReg(manaCost.transform, RegDataType.Cost);
        }

        /// <summary>
        /// Animates a data object from spawn point through RAM to the target register
        /// </summary>
        /// <param name="data">The data transform to move</param>
        /// <param name="type">The data type to assign</param>
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
                    
                    reg.regParent = reg4;
                    draggable.draggingEnabled = true;
                });
            });
            
            RegTrigger rt = reg4.GetComponent<RegTrigger>();
            draggable.DragEnd += () => rt.SnapDataToReg(reg);
        }

        /// <summary>
        /// Animates the wizard shield data back to RAM and triggers finish state check
        /// </summary>
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
