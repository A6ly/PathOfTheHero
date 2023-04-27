using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;

    Character character;
    Vector2Int positionOnGrid;

    public void StartBattle()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            character = transform.GetChild(i).gameObject.GetComponent<Character>();
            battleManager.CheckWalkableGround(character, false);
            positionOnGrid = character.GetComponent<GridObject>().positionOnGrid;
            List<KeyValuePair<int, Vector2Int>> targets = StageManager.Instance.StageGrid.GetCharacterPeripheralPosition(positionOnGrid);
        }
    }
}