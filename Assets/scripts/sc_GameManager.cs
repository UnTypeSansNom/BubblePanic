using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class sc_GameManager : MonoBehaviour
{
    public static sc_GameManager instance;
    public Transform ennemiesParent;
    public List<GameObject> enemies;

    [Header("Paramètres de Spawn")]
    public float offsetUp, offsetRight, maxCooldownSpawn;
    public TMP_Text affichage;

    Ray ray;
    RaycastHit hit;
    float _cooldown;

    int _score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Tow gameManagers detected");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _cooldown = maxCooldownSpawn;
        StartCoroutine(TutoLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Ball")))
            {
                hit.transform.GetComponent<sc_TargetClic>().ReceiveOrder();
            }
        }
    }

    public void GetScore(int nb)
    {
        _score += nb;
        affichage.text = "Score : " + _score.ToString();
    }


    IEnumerator MainLoop()
    {
        yield return new WaitForSeconds(_cooldown);
        Vector3 offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        int rd = Random.Range(0, enemies.Count);
        Instantiate(enemies[rd], ennemiesParent.position + offset, enemies[rd].transform.rotation, ennemiesParent);

        if(_cooldown > maxCooldownSpawn * 0.5f)
        {
            _cooldown *= 0.9f;
        }
        StartCoroutine(MainLoop());
    }

    IEnumerator TutoLoop()
    {
        //2 bulles
        Vector3 offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[0], ennemiesParent.position + offset, enemies[0].transform.rotation, ennemiesParent);
        yield return new WaitForSeconds(maxCooldownSpawn);
        offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[0], ennemiesParent.position + offset, enemies[0].transform.rotation, ennemiesParent);

        yield return new WaitForSeconds(maxCooldownSpawn); // 2 cubes
        offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[1], ennemiesParent.position + offset, enemies[1].transform.rotation, ennemiesParent);
        yield return new WaitForSeconds(maxCooldownSpawn);
        offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[1], ennemiesParent.position + offset, enemies[1].transform.rotation, ennemiesParent);


        yield return new WaitForSeconds(maxCooldownSpawn); // 1 bulle
        offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[0], ennemiesParent.position + offset, enemies[0].transform.rotation, ennemiesParent);

        yield return new WaitForSeconds(maxCooldownSpawn); // 2 Chenilles
        offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[2], ennemiesParent.position + offset, enemies[2].transform.rotation, ennemiesParent);
        yield return new WaitForSeconds(maxCooldownSpawn);
        offset = new Vector3(Random.Range(0f, offsetRight), Random.Range(0f, offsetUp), 0f);
        Instantiate(enemies[2], ennemiesParent.position + offset, enemies[2].transform.rotation, ennemiesParent);
        StartCoroutine(MainLoop());
    }
}
