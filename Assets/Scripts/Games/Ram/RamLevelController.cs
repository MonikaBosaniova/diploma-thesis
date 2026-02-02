using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class RamLevelController : LevelController
    {
        [Header("Ram Level")] 
        [SerializeField] private float Time;
        [SerializeField] private GameObject Shape;
        [SerializeField] private GameObject SpawnShapesParent;
        [SerializeField] private GameObject ShapesDoneParent;
        [SerializeField] private GameObject CPUParent;
        
        RamGameManager ramGameManager;
        private List<ShapeController> _allSnappedShapes = new List<ShapeController>();
        private bool generateNewShape = false;
        
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

        private void Update()
        {
            if (generateNewShape)
            {
                GenerateWantedShape();
                generateNewShape = false;
            }
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
            if (CPUParent.transform.childCount != 1)
            {
                generateNewShape = true;
            }
        }

        public void GenerateWantedShape()
        {
            if(_allSnappedShapes.Count == 0) return;
            
            var random = Random.Range(0, _allSnappedShapes.Count);
            var copy = Instantiate(_allSnappedShapes[random].gameObject, CPUParent.transform);
            copy.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        public void AddShapeToSnappedList(ShapeController shapeController)
        {
            _allSnappedShapes.Add(shapeController);
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
