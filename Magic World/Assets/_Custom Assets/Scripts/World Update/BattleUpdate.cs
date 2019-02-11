using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "World/Battle Update")]
public class BattleUpdate : WorldUpdate
{
    public float turnTime = 5;

    public override void StartWorld(World world)
    {
        //world.FindAllCharacters();
        for (int i = 0; i < world.allCharacters.Count; i++) {
            CharacterManager character = world.allCharacters[i];

            if (character.tag == "enemy")
            {
                world.enemies.Add(character);
            }
            else {
                world.players.Add(character);
            }
        }

        SetTurnOrder(world);

        NextTurn(world);
    }

    public override void UpdateWorld(World world)
    {
        world.currentTurn.TakeTurn();
        if (world.turnTimer > 0)
        {
            world.turnTimer -= Time.deltaTime;
        }
        else {
            NextTurn(world);
        }
    }

    public override void EndWorld(World world)
    {
        
    }

    public void NextTurn(World world) {
        world.turnIndex = (world.turnIndex + 1) % world.turnOrder.Count;
        world.currentTurn = world.turnOrder[world.turnIndex];
        world.turnTime = turnTime;
        world.turnTimer = turnTime;
    }

    public void SetTurnOrder(World world) {
        world.turnOrder = new List<CharacterManager>();

        for (int i = 0; i < world.allCharacters.Count; i++) {
            world.turnOrder.Add(world.allCharacters[i]);
        }

    }
}
