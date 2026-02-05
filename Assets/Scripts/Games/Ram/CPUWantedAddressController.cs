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
            Debug.Log("RANDOM: R-" +allPossibleAddresses[random].Item1 + ",   C-" + allPossibleAddresses[random].Item2);
            
            //VISUALIZE ADDRESS
            string row = addressesRowParent.GetChild(allPossibleAddresses[random].Item1).GetComponent<TMP_Text>().text;
            string col = addressesColumnParent.GetChild(allPossibleAddresses[random].Item2).GetComponent<TMP_Text>().text;
            addressRow.text = row;
            addressColumn.text = col;
            Debug.Log("CPU WANTED: R-" + row + "  C-" + col);
        }

        internal void SetRamLevelController(RamLevelController ramLevelController)
        {
            _ramLevelController = ramLevelController;
        } 

        private void OnTriggerEnter(Collider other)
        {
            if (generateNewAddress) return;

            SnappedAddressCubieController cubie = other.gameObject.GetComponent<SnappedAddressCubieController>();
            
            bool answerIsCorrect = CheckAddress(cubie);
            
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

        private bool CheckAddress(SnappedAddressCubieController cubie)
        {
            
            throw new NotImplementedException();
        }
        
    }
    
}
