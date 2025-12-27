using UnityEngine;

namespace Games.Ram
{
    public class RamLevelController : LevelController
    {
        [Header("Ram Level")] 
        [SerializeField] private GameObject Shape;
        [SerializeField] private GameObject SpawnShapesParent;
        [SerializeField] private GameObject ShapesDoneParent;
        
        RamGameManager ramGameManager;
        
        public override void Init()
        {
            ramGameManager = FindFirstObjectByType<RamGameManager>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            GenerateShape();
            
            base.Init();
        }

        public void GenerateShape()
        {
            if (Shape != null)
            {
                Shape.transform.parent = ShapesDoneParent.transform;
                Shape = null;
            }
            
            Shape = Instantiate(ramGameManager.allPossibleGeneratedShapes[0], SpawnShapesParent.transform);
            Shape.GetComponent<ShapeController>().SetLevelController(this);
            Debug.Log("Generate shape");
        }

        private void CheckFinishState(double newValue)
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }

        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }

        private void ShowDecNumber(int numToShow)
        {
            
        }

        private void ShowNumberOnSpecificIndex(int indexInDisplay, int numToShow)
        {

        }
        
    }
}
