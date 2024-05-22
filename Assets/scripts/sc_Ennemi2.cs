using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Ennemi2 : MonoBehaviour
{
    public float speed = 1f;

    Vector3 _startPos;
    float _offset;

    Renderer _mat;
    float _chop = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        speed = Random.Range(speed * 0.8f, speed * 1.2f);
        _offset = Random.Range(0f, 3.14f);
        _mat = GetComponent<Renderer>();
        _mat.material.SetFloat("_ChopStrenght", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time * speed + _offset;
        transform.position = _startPos + new Vector3(Mathf.Sin(2 * t), Mathf.Cos(3 * t), 0);
    }

    public void Chop()
    {
        if(_chop <= 0.5f)
        {
            _chop += 0.4f;
            _mat.material.SetFloat("_ChopStrenght", _chop);
        }
        else
        {
            sc_GameManager.instance.GetScore(300);
            Destroy(gameObject);
        }
    }
}
