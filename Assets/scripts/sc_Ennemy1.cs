using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Ennemy1 : MonoBehaviour
{
    // Cet ennemi ne bouge pas, on travail bcp la texture pr compenser

    Renderer _mat;

    float bounce = 0.005f;

    private void Start()
    {
        _mat = GetComponent<Renderer>();
        _mat.material.SetFloat("_WaveStrenght", 0.005f);
    }



    public void Popped()
    {
        if(bounce <= 0.005f)
        {
            StartCoroutine(BounceEffect());
        }
        else
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator BounceEffect()
    {
        WaitForSeconds tempo = new WaitForSeconds(1f / 60f);
        bounce = 0.05f;
        for(int i = 0; i < 120;  i++)
        {
            _mat.material.SetFloat("_WaveStrenght", bounce);
            bounce -= (0.05f / 120f);
            yield return tempo;
        }
        bounce = 0.005f;
    }

    IEnumerator Death()
    {
        sc_GameManager.instance.GetScore(100);
        WaitForSeconds tempo = new WaitForSeconds(1f / 60f);
        for (int i = 0; i < 5; i++)
        {
            _mat.material.SetFloat("_PopStrenght", i/5f);
            yield return tempo;
        }
        Destroy(gameObject);
    }
}
