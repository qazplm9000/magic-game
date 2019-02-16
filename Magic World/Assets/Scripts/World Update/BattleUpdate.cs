using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "World/Battle Update")]
public class BattleUpdate : WorldUpdate
{
    public float turnTime = 5;

    public override void StartWorld(World world)
    {

        SetTurnOrder(world);

        NextTurn(world);
    }

    public override void UpdateWorld(World world)
    {
        //checks if anyone is currently casting anything
        bool isCasting = false;

        //loops through all characters and updates them
        /*for (int i = 0; i < world.allCharacters.Count; i++) {
            isCasting = world.allCharacters[i].TakeTurn() || isCasting;
        }*/

        if (world.turnTimer > 0)
        {
            world.turnTimer -= Time.deltaTime;
        }
        else {
            if (!isCasting)
            {
                NextTurn(world);
            }
        }
    }

    public override void EndWorld(World world)
    {
        
    }

    public void NextTurn(World world) {
        world.turnIndex = (world.turnIndex + 1) % world.turnOrder.Count;
        world.currentTurn = world.turnOrder[world.turnIndex];
        world.turnTime = world.currentTurn.stats.attackTime;
        world.turnTimer = world.turnTime;
    }

    public void SetTurnOrder(World world) {
        world.turnOrder = new List<CharacterManager>();

        for (int i = 0; i < world.allCharacters.Count; i++) {
            world.turnOrder.Add(world.allCharacters[i]);
        }

    }
}
