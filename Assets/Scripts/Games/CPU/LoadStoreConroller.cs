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
        
        /// <summary>
        /// Loads the ManaLeft data from RAM into a register
        /// </summary>
        public void GetManaLeftFromRAM()
        {
            ramController.SpawnGetManaLeftData();
        }

        /// <summary>
        /// Loads the ManaCost data from RAM into a register
        /// </summary>
        public void GetManaCostFromRAM()
        {
            ramController.SpawnGetManaCostData();
        }

        /// <summary>
        /// Stores the wizard shield data back to RAM
        /// </summary>
        public void StoreManaLeftToRAM()
        {
            ramController.DespawnWizardShield();
        }
        
    }
}
