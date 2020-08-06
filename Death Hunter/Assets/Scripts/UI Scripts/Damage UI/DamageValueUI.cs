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
    public List<Vector3> damageLocations = new List<Vector3>();
    public Vector3 offset;
    public static int lastLocation = -1;
    public float parallaxModifier = 5;
    [Range(0,2)]
    public float scaleModifier = 2;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick();
        Animate();
        if (timer.AtTime(lifetime))
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

        GenerateRandomOffset();
        SetPosition(target.transform.position);
    }

    private void Animate()
    {
        Vector3 targetPosition = target.transform.position;
        SetPosition(targetPosition);

        ScaleBasedOnDistance(targetPosition);
    }

    private void SetPosition(Vector3 targetPosition)
    {
        transform.position = WorldManager.world.cam.WorldToScreenPoint(targetPosition);
        transform.position += offset;
        transform.position += timer.GetCurrentTime() * new Vector3(0, 1, 0) * speed;
    }

    private void GenerateRandomOffset()
    {
        int rand = lastLocation;

        while (rand == lastLocation)
        {
            rand = Random.Range(0, damageLocations.Count);
        }
        lastLocation = rand;
        offset = damageLocations[rand];
    }

    private float GetDistanceFromCamera(Vector3 position)
    {
        return (WorldManager.world.cam.transform.position - position).magnitude;
    }

    private void ScaleBasedOnDistance(Vector3 position)
    {
        float distance = GetDistanceFromCamera(position);
        float scale = scaleModifier * (1 + parallaxModifier) / (distance + parallaxModifier);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
