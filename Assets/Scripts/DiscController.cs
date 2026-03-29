using UnityEngine;

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

    void PickConfig()
    {
        var i = Random.Range(0, speeds.Length);
        speed = speeds[i];
        sectorCount = chunks[Mathf.Clamp(i, 0, chunks.Length - 1)];
        ringIndex = Random.Range(0, discs.Length);
        sectorIndex = Random.Range(0, sectorCount);
    }

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

    void Rotate()
    {
        RotatingPart.transform.Rotate(0f, speed * 360f * Time.deltaTime, 0f, Space.Self);
    }

    bool IsNearZeroAngle(float degrees)
    {
        var y = RotatingPart.transform.eulerAngles.y;
        return Mathf.Abs(Mathf.DeltaAngle(y, 0f)) <= degrees;
    }

    bool IsReadAligned()
    {
        if (!Head || !flag) return false;
        return IsSectorAligned() && IsRingAligned();
    }

    bool IsSectorAligned()
    {
        var headAng = WorldAngleXZ(Head.transform.position);
        var flagAng = WorldAngleXZ(flag.position);
        return Mathf.Abs(Mathf.DeltaAngle(headAng, flagAng)) <= (180f / sectorCount);
    }

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

    void ReadingSuccessful()
    {
    }
}
