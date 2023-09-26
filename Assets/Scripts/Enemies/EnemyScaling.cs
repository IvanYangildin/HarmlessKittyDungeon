using UnityEngine;

[RequireComponent(typeof(StuckDetector))]
public class EnemyScaling : MonoBehaviour
{
    private StuckDetector detector;

    // higher value - less effective weapon
    [SerializeField]
    [Min(0.1f)]
    private float armor = 1f;

    public float MinSize, MaxSize;
    private float currentSize = 1f;
    private float finalSize;

    public float FinalScale
    {
        set { finalSize = Mathf.Clamp(value, MinSize, MaxSize); }
        get { return finalSize; }
    }

    [SerializeField]
    [Min(0.1f)]
    private float scaleSpeed;

    private void Awake()
    {
        currentSize = transform.localScale.x;
        finalSize = currentSize;
        detector = GetComponent<StuckDetector>();
    }

    public void AddToFinalSize(float deltaSize)
    {
        finalSize = Mathf.Clamp(currentSize + deltaSize/armor, MinSize, MaxSize);
    }

    private void FixedUpdate()
    {
        currentSize = transform.localScale.z;
        float s = Mathf.Sign(finalSize - currentSize);
        bool stuck = false;
        if (finalSize > currentSize)
        {
            stuck = detector.StuckCalculate(); 
        }
        if (!stuck)
        {
            float deltaSize = scaleSpeed * Time.deltaTime;
            currentSize += s * deltaSize;
            currentSize = (currentSize + finalSize - Mathf.Abs(finalSize - currentSize) * s) / 2;
            transform.localScale = currentSize * Vector3.one;
        }
    }

}