#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CubeGridEditor : MonoBehaviour
{
    public GameObject cubePrefab;
    public int x = 3, y = 3, z = 3;
    public float spacing = 1f;

    [ContextMenu("Generate Grid")]
    public void Generate()
    {
        Debug.Log("Generate Grid called");

        if (cubePrefab == null)
        {
            Debug.LogError("Cube prefab is NULL");
            return;
        }

        // Odstránime existujúce deti
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Generovanie gridu
        for (int i = 0; i < x; i++)
        for (int j = 0; j < y; j++)
        for (int k = 0; k < z; k++)
        {
            Vector3 pos = new Vector3(i, j, k) * spacing + transform.position;

            // TOTO je kľúč: Instantiate priamo do scény
            GameObject cube = (GameObject)PrefabUtility.InstantiatePrefab(cubePrefab);
            cube.transform.position = pos;
            cube.transform.rotation = Quaternion.identity;
            cube.transform.SetParent(transform);

            Undo.RegisterCreatedObjectUndo(cube, "Create Cube");
        }

        // Označíme scénu ako dirty, aby sa uložilo
        EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }
}
#endif