using UnityEngine;
using Unity.VectorGraphics;
using UnityEngine.UIElements;

public class LineRenderer : MonoBehaviour
{

    // private void OnEnable()
    // {
    //     // Initialize UI Toolkit root element
    //     var uiDocument = GetComponent<UIDocument>();
    //     rootElement = uiDocument.rootVisualElement;
    //
    //     // Create and add the curved line
    //     CreateCurvedLine();
    // }
    //
    // private void CreateCurvedLine()
    // {
    //     // Define Bezier points
    //     var p0 = new Vector2(0, 0);    // Start point
    //     var p1 = new Vector2(100, 200); // Control point 1
    //     var p2 = new Vector2(200, 200); // Control point 2
    //     var p3 = new Vector2(300, 0);  // End point
    //
    //     // Create the bezier curve path
    //     var path = new BezierPathSegment[]
    //     {
    //         new BezierPathSegment(p0, p1, p2, p3)
    //     };
    //
    //     // Define the shape using the path
    //     var shape = new Shape()
    //     {
    //         Contours = new[] { new BezierContour() { Segments = path, Closed = false } },
    //         PathProps = new PathProperties()
    //         {
    //             Stroke = new Stroke()
    //             {
    //                 Color = Color.red,
    //                 HalfThickness = 2.0f
    //             }
    //         }
    //     };
    //
    //     // Render the shape to a Texture2D
    //     var options = new VectorUtils.TessellationOptions()
    //     {
    //         StepDistance = 1.0f,
    //         MaxCordDeviation = 0.5f,
    //         SamplingStepSize = 0.1f
    //     };
    //     var sceneNode = new SceneNode() { Shapes = new[] { shape } };
    //     var tessellatedScene = VectorUtils.TessellateScene(sceneNode, options);
    //     var texture = VectorUtils.RenderToTexture2D(tessellatedScene, 512, 512, new Vector2(0.5f, 0.5f));
    //
    //     // Create a UI element and set the texture as its background
    //     var curveElement = new VisualElement();
    //     curveElement.style.backgroundImage = new StyleBackground(texture);
    //     curveElement.style.width = 300;
    //     curveElement.style.height = 300;
    //
    //     // Add the element to the UI
    //     rootElement.Add(curveElement);
    //}
}
