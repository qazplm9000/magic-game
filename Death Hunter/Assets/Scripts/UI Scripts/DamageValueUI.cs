using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageValueUI : MonoBehaviour
{
    public TMP_Text text;
    public int damage;
    public Combatant target;

    public float lifetime = 1;
    public Timer timer;
    public float speed = 2000;
    public float maxRNG = 100;
    public Vector3 offset;
    public float parallaxModifier = 5;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        GenerateRandomOffset();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick();
        Animate();
        if (timer.AtTime(2))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetupDamage(Combatant target, int damage)
    {
        this.target = target;
        this.damage = damage;
        text.text = damage.ToString();
        timer = new Timer();
    }

    private void Animate()
    {
        Vector3 targetPosition = target.transform.position;
        Debug.Log(transform.position);
        transform.position = WorldManager.world.cam.WorldToScreenPoint(targetPosition);
        transform.position += offset;
        transform.position += timer.GetCurrentTime() * new Vector3(0, 1, 0) * speed;
        Debug.Log(transform.position);

        ScaleBasedOnDistance(targetPosition);
    }

    private void GenerateRandomOffset()
    {
        float randX = Random.Range(-maxRNG, maxRNG);
        float randY = Random.Range(-maxRNG, maxRNG);
        offset = new Vector3(randX, randY, 0);
    }

    private float GetDistanceFromCamera(Vector3 position)
    {
        return (WorldManager.world.cam.transform.position - position).magnitude;
    }

    private void ScaleBasedOnDistance(Vector3 position)
    {
        float distance = GetDistanceFromCamera(position);
        float scale = 2 * (1 + parallaxModifier) / (distance + parallaxModifier);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
