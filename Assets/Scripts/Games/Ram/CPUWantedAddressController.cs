using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class CPUWantedAddressController : MonoBehaviour
    {
        public bool generateNewAddress = true;
        
        [Header("Answers")]
        [SerializeField] private GameObject correctAnswer;
        [SerializeField] private GameObject wrongAnswer;
        
        [Header("Address")]
        [SerializeField] private TMP_Text addressRowTextBox;
        [SerializeField] private TMP_Text addressColumnTextBox;
        [SerializeField] private Transform addressesRowParent;
        [SerializeField] private Transform addressesColumnParent;

        [Header("---DEBUG---")] 
        [SerializeField] private int rowAddress;
        [SerializeField] private int columnAddress;
        
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
            rowAddress = allPossibleAddresses[random].Item1;
            columnAddress = allPossibleAddresses[random].Item2;
            
            //VISUALIZE ADDRESS
            string row = addressesRowParent.GetChild(rowAddress).GetComponent<TMP_Text>().text;
            string col = addressesColumnParent.GetChild(columnAddress).GetComponent<TMP_Text>().text;
            addressRowTextBox.text = row;
            addressColumnTextBox.text = col;
        }

        internal void SetRamLevelController(RamLevelController ramLevelController)
        {
            _ramLevelController = ramLevelController;
        } 

        internal void CheckAddress(SnappedAddressCubieController cubie)
        {
            Tuple<int, int> cubieAddress = cubie.GetAddress();
            bool answerIsCorrect =  cubieAddress != null && 
                                    cubieAddress.Item1 == rowAddress &&
                                    cubieAddress.Item2 == columnAddress;
            
            // //TODO NICER VISUALS
            if (answerIsCorrect)
            {
                addressRowTextBox.text = "";
                addressColumnTextBox.text = "";
                _bgCubeTrigger.SetHighlight(false);
                _bgCubeTrigger.highlightingEnabled = false;
                correctAnswer.SetActive(true);
                Debug.Log("CORRECT");
                StartCoroutine(ShowAnswer(true));
            }
            else
            {
                _snappedAddressCubieController.RemoveCPUPoint();
                _snappedAddressCubieController.Snap();
                wrongAnswer.SetActive(true);
                Debug.Log("WRONG");
                StartCoroutine(ShowAnswer(false));
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {

            if (generateNewAddress) return;

            _snappedAddressCubieController = other.gameObject.GetComponent<SnappedAddressCubieController>();
            _snappedAddressCubieController.SetCPUPoint(transform);
            if (_snappedAddressCubieController != null) _bgCubeTrigger.SetHighlight(true);
        }

        private void OnTriggerExit(Collider other)
        {
            _snappedAddressCubieController = other.gameObject.GetComponent<SnappedAddressCubieController>();
            if (_snappedAddressCubieController == null) return;
            
            _bgCubeTrigger.SetHighlight(false);
            _snappedAddressCubieController.RemoveCPUPoint();
            _snappedAddressCubieController = null;
        }
        
        private IEnumerator ShowAnswer(bool isCorrect) {
            yield return new WaitForSeconds(2f);

            if (isCorrect)
            {
                generateNewAddress = true;
                _ramLevelController.GenerateShape();
                _snappedAddressCubieController.RemoveCPUPoint();
                _snappedAddressCubieController.Snap();
                correctAnswer.SetActive(false);
            }
            else
            {
                wrongAnswer.SetActive(false);
            }
        }
    }
}
