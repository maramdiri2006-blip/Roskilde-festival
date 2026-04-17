using UnityEngine;

public class IntoxicationSystem : MonoBehaviour
{
    [Range(0f, 1f)]
    public float intoxication = 0f;

    public float increaseAmount = 0.2f;
    public float decreaseSpeed = 0.05f;

    void Update()
    {
        // Tryk på E for at drikke
        if (Input.GetKeyDown(KeyCode.E))
        {
            intoxication += increaseAmount;
        }

        // Falder langsomt over tid
        intoxication -= decreaseSpeed * Time.deltaTime;

        intoxication = Mathf.Clamp01(intoxication);
    }
}