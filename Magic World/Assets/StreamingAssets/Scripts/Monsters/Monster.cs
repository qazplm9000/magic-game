using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monsters/Monster")]
public class Monster : ScriptableObject {

    public string monsterName;
    public CharacterData stats;
    public GameObject monsterModel;
    public Texture texture;
}
