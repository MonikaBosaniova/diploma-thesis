using UnityEngine;

/// <summary>
/// Simulates the HDD disc mechanism with rotating platters, data flags and read head alignment
/// </summary>
public class DiscController : MonoBehaviour
{
    public GameObject Cylinder;
    public GameObject DataFlag;
    public GameObject Head;
    public GameObject RotatingPart;

    public Material DiscOddMaterial;
    public Material DiscEvenMaterial;

    int[] chunks = { 4, 6, 8, 10 };
    float[] speeds = { 1f, 2f, 4f, 8f };

    readonly Transform[] discs = new Transform[4];
    Transform flag;
    int ringIndex, sectorIndex, sectorCount;
    float speed;

    void Start()
    {
        GenerateDiscs();
        PickConfig();
        SpawnReadFlag();
        StartRotation();
    }

    void Update()
    {
        Rotate();
        if (IsNearZeroAngle(5f) && IsReadAligned()) ReadingSuccessful();
    }

    /// <summary>
    /// Generates disc platters as children of the rotating part with alternating materials
    /// </summary>
    void GenerateDiscs()
    {
        var parent = RotatingPart.transform;
        var baseScale = Cylinder ? Cylinder.transform.localScale.x : 1f;

        for (int i = 0; i < discs.Length; i++)
        {
            var go = Cylinder ? Instantiate(Cylinder, parent) : GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.name = $"Disc_{i}";
            var t = go.transform;

            t.localPosition = new Vector3(0f, i * 0.01f, 0f);

            var s = baseScale * ((discs.Length - i) / (float)discs.Length);
            t.localScale = new Vector3(s, t.localScale.y, s);

            var r = go.GetComponent<Renderer>();
            if (r) r.material = (i % 2 == 0) ? DiscEvenMaterial : DiscOddMaterial;

            discs[i] = t;
        }
    }

    /// <summary>
    /// Randomly selects rotation speed, sector count, ring and sector index for the current configuration
    /// </summary>
    void PickConfig()
    {
        var i = Random.Range(0, speeds.Length);
        speed = speeds[i];
        sectorCount = chunks[Mathf.Clamp(i, 0, chunks.Length - 1)];
        ringIndex = Random.Range(0, discs.Length);
        sectorIndex = Random.Range(0, sectorCount);
    }

    /// <summary>
    /// Spawns the data flag on the selected ring and sector position
    /// </summary>
    void SpawnReadFlag()
    {
        var parent = RotatingPart.transform;
        var go = DataFlag ? Instantiate(DataFlag, parent) : new GameObject("DataFlag");
        flag = go.transform;

        var ringScale = discs[ringIndex].lossyScale.x;
        flag.position = new Vector3(0f, 1f, ringScale);

        var angle = SectorAngle(sectorIndex, sectorCount);
        flag.localPosition += OffsetOnRing(angle, 0.5f * ringScale);
    }

    void StartRotation() { }

    /// <summary>
    /// Rotates the disc platters around the Y axis at the configured speed
    /// </summary>
    void Rotate()
    {
        RotatingPart.transform.Rotate(0f, speed * 360f * Time.deltaTime, 0f, Space.Self);
    }

    /// <summary>
    /// Checks if the disc rotation is near the zero angle within the specified tolerance
    /// </summary>
    /// <param name="degrees">Tolerance in degrees</param>
    /// <returns>True if the rotation angle is within tolerance of zero</returns>
    bool IsNearZeroAngle(float degrees)
    {
        var y = RotatingPart.transform.eulerAngles.y;
        return Mathf.Abs(Mathf.DeltaAngle(y, 0f)) <= degrees;
    }

    /// <summary>
    /// Checks if the read head is aligned with the data flag in both sector and ring
    /// </summary>
    /// <returns>True if the head is aligned for reading</returns>
    bool IsReadAligned()
    {
        if (!Head || !flag) return false;
        return IsSectorAligned() && IsRingAligned();
    }

    /// <summary>
    /// Checks if the head and flag are in the same angular sector
    /// </summary>
    /// <returns>True if sector-aligned</returns>
    bool IsSectorAligned()
    {
        var headAng = WorldAngleXZ(Head.transform.position);
        var flagAng = WorldAngleXZ(flag.position);
        return Mathf.Abs(Mathf.DeltaAngle(headAng, flagAng)) <= (180f / sectorCount);
    }

    /// <summary>
    /// Checks if the head and flag are on the same ring (track)
    /// </summary>
    /// <returns>True if ring-aligned</returns>
    bool IsRingAligned()
    {
        var headR = RadiusXZ(Head.transform.position);
        var flagR = RadiusXZ(flag.position);
        var tol = 0.5f * (discs[0].lossyScale.x / discs.Length);
        return Mathf.Abs(headR - flagR) <= tol;
    }

    float SectorAngle(int sector, int count) => sector * (360f / count);

    Vector3 OffsetOnRing(float angleDeg, float radius)
    {
        var rad = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad) * radius, 0f, Mathf.Cos(rad) * radius);
    }

    float WorldAngleXZ(Vector3 p) => Mathf.Atan2(p.x, p.z) * Mathf.Rad2Deg;

    float RadiusXZ(Vector3 p) => new Vector2(p.x, p.z).magnitude;

    /// <summary>
    /// Called when the read head successfully aligns with the data flag
    /// </summary>
    void ReadingSuccessful()
    {
    }
}
