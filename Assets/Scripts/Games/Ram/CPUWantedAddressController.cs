using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class CPUWantedAddressController : MonoBehaviour
    {
        public bool generateNewAddress = true;
        
        [Header("Address")]
        [SerializeField] private TMP_Text addressRow;
        [SerializeField] private TMP_Text addressColumn;
        [SerializeField] private Transform addressesRowParent;
        [SerializeField] private Transform addressesColumnParent;
        
        private RamLevelController _ramLevelController;
        private BgCubeTrigger _bgCubeTrigger;
        private SnappedAddressCubieController _snappedAddressCubieController;

        private void Start()
        {
            _bgCubeTrigger = gameObject.GetComponent<BgCubeTrigger>();
            _bgCubeTrigger.highlightingEnabled = false;
        }

        public void GenerateWantedAddress(List<Tuple<int,int>> allPossibleAddresses)
        {
            //List<Tuple<int,int>> allPossibleAddresses = GetAllAddresses();
            if (allPossibleAddresses.Count == 0) return;
            
            _bgCubeTrigger.highlightingEnabled = true;
            generateNewAddress = false;
            var random = Random.Range(0, allPossibleAddresses.Count);
            
            //VISUALIZE ADDRESS
            string row = addressesRowParent.GetChild(allPossibleAddresses[random].Item1).GetComponent<TMP_Text>().text;
            string col = addressesColumnParent.GetChild(allPossibleAddresses[random].Item2).GetComponent<TMP_Text>().text;
            addressRow.text = row;
            addressColumn.text = col;
        }

        internal void SetRamLevelController(RamLevelController ramLevelController)
        {
            _ramLevelController = ramLevelController;
        } 

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("CPU: OnTriggerEnter   ...  " + other.gameObject.name + "...gen: "+ generateNewAddress);

            if (generateNewAddress) return;

            _snappedAddressCubieController = other.gameObject.GetComponent<SnappedAddressCubieController>();
            _snappedAddressCubieController.SetCPUPoint(transform);
            if (_snappedAddressCubieController != null) _bgCubeTrigger.SetHighlight(true);
            
            bool answerIsCorrect = CheckAddress(_snappedAddressCubieController);
            
            //TODO NICER VISUALS
            if (answerIsCorrect)
            {
                _bgCubeTrigger.highlightingEnabled = false;
                addressRow.text = "";
                addressColumn.text = "";
                generateNewAddress = true;
                _ramLevelController.GenerateShape();
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _snappedAddressCubieController = other.gameObject.GetComponent<SnappedAddressCubieController>();
            if (_snappedAddressCubieController == null) return;
            
            _bgCubeTrigger.SetHighlight(false);
            _snappedAddressCubieController.RemoveCPUPoint();
            _snappedAddressCubieController = null;
        }

        private bool CheckAddress(SnappedAddressCubieController cubie)
        {
            
            throw new NotImplementedException();
        }
        
    }
    
}
