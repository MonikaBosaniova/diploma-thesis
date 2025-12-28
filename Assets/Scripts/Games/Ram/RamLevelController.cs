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
            
            var random = Random.Range(0, ramGameManager.allPossibleGeneratedShapes.Count);
            Shape = Instantiate(ramGameManager.allPossibleGeneratedShapes[random], SpawnShapesParent.transform);
            Shape.GetComponent<ShapeController>().SetLevelController(this);
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
        
    }
}
