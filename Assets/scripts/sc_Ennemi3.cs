using System.Collections;
using UnityEngine;

public class sc_Ennemi3 : MonoBehaviour
{
    public GameObject[] Parts = new GameObject[5];
    public float minH, maxH, minL, maxL;
    public float speed, earlyDamp, lateDamp;

    Vector3 _startPos, _targetPos;
    float travel_time = 0;
    float speedmult = 1;

    bool canReset = true;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _targetPos = new Vector3(Random.Range(minL, maxL), Random.Range(minH, maxH), 0.7f);
        travel_time = 0;
        speed = transform.GetChild(0).GetComponent<sc_TargetClic>().travelDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            sc_GameManager.instance.GetScore(500);
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, _targetPos) < 0.001f)
        {
            if (canReset)
            {
                StartCoroutine(ResetLoop());
            }
        }
        travel_time += Time.deltaTime;

        float neoRatio2 = 1 - Mathf.Pow(1 - Mathf.Pow(Mathf.Clamp01(travel_time / (speed * speedmult)), earlyDamp), lateDamp);
        transform.position = Vector3.Lerp(_startPos, _targetPos, neoRatio2);

        for (int i = 0; i < transform.childCount; i++)
        {
            float neoRatio = 1 - Mathf.Pow(1 - Mathf.Pow(Mathf.Clamp01(travel_time / ((speed * speedmult) + 0.1f * i)), earlyDamp), lateDamp);
            transform.GetChild(i).position = Vector3.Lerp(_startPos, _targetPos, neoRatio);
            transform.GetChild(i).LookAt(_targetPos, Vector3.up);
        }
    }

    /// <summary>
    /// Pour laisser le temps aux membres d'arriver
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetLoop()
    {
        Debug.Log("loop");
        canReset = false;
        yield return new WaitForSeconds(speedmult * 1.5f);
        _startPos = transform.position;
        _targetPos = new Vector3(Random.Range(minL, maxL), Random.Range(minH, maxH), 0.7f);
        travel_time = 0;
        canReset = true;
    }

    public void Clicked()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        speedmult -= 0.1f;
        earlyDamp += 0.5f;
        lateDamp += 0.5f;
    }
}
