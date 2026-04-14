using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Class with public methods for buttons in Load/Store
    /// component in the mini game
    /// </summary>
    public class LoadStoreConroller : MonoBehaviour
    {
        [SerializeField] private RamController ramController;
        
        public void GetManaLeftFromRAM()
        {
            ramController.SpawnGetManaLeftData();
        }

        public void GetManaCostFromRAM()
        {
            ramController.SpawnGetManaCostData();
        }

        public void StoreManaLeftToRAM()
        {
            ramController.DespawnWizardShield();
        }
        
    }
}
