using UnityEngine;

namespace Games.CPU
{
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
            Debug.Log("Store Mana left");
        }
        
    }
}
