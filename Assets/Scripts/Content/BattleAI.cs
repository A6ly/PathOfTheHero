using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI : MonoBehaviour
{
    public void StartBattle()
    {
        StartCoroutine(AutoBattle());
    }

    private void Attack(CharacterController characterController, CharacterTurn characterTurn, GridObject targetGridObject)
    {
        characterController.Attack(targetGridObject);
        characterTurn.canAttack = false;
        StageManager.Instance.ClearAttackHighlight();
    }

    private void Move(CharacterController characterController, CharacterTurn characterTurn, List<PathNode> path)
    {
        characterController.Move(path);
        characterTurn.canMove = false;
        StageManager.Instance.ClearPathFinder();
        StageManager.Instance.ClearMoveHighlight();
    }

    private IEnumerator AutoBattle()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (TurnManager.Instance.isEndStage)
            {
                yield break;
            }

            Character character = transform.GetChild(i).gameObject.GetComponent<Character>();

            if (character != null && character.gameObject.activeSelf)
            {
                if (character.isDead)
                {
                    continue;
                }

                CharacterController characterController = character.GetComponent<CharacterController>();
                CharacterTurn characterTurn = character.GetComponent<CharacterTurn>();
                GridObject gridObject = character.GetComponent<GridObject>();
                Vector2Int positionOnGrid = gridObject.positionOnGrid;
                GridObject targetGridObject = StageManager.Instance.StageGrid.GetPeripheralPlayer(positionOnGrid);

                if (targetGridObject != null && characterTurn.canAttack)
                {
                    BattleManager.Instance.CalculateAttackArea(gridObject.positionOnGrid, character.stat.AttackRange, character.tag);
                    EffectManager.Instance.HighlightPointEffect(gridObject.positionOnGrid, character.tag);

                    yield return new WaitForSeconds(2.0f);

                    EffectManager.Instance.HidePointEffect(character.tag);
                    Attack(characterController, characterTurn, targetGridObject);

                    yield return new WaitForSeconds(5.0f);
                }
                else
                {
                    BattleManager.Instance.CalculateWalkableGround(character, false);
                    List<KeyValuePair<int, Vector2Int>> targets = StageManager.Instance.StageGrid.GetPlayerPeripheralPosition(positionOnGrid);

                    foreach (var target in targets)
                    {
                        Vector2Int targetPositionOnGrid = target.Value;
                        List<PathNode> path = BattleManager.Instance.GetPath(targetPositionOnGrid);

                        if (path != null && characterTurn.canMove && characterTurn.canAttack)
                        {
                            BattleManager.Instance.CalculateWalkableGround(character, true);
                            EffectManager.Instance.HighlightPointEffect(gridObject.positionOnGrid, character.tag);

                            yield return new WaitForSeconds(2.0f);

                            EffectManager.Instance.HidePointEffect(character.tag);
                            Move(characterController, characterTurn, path);
                            Vector3 targetWorldPos = StageManager.Instance.StageGrid.GetWorldPosition(targetPositionOnGrid.x, targetPositionOnGrid.y, true);
                            while (Vector3.Distance(character.transform.position, targetWorldPos) > 0.1f)
                            {
                                yield return new WaitForSeconds(1.0f);
                            }

                            BattleManager.Instance.CalculateAttackArea(gridObject.positionOnGrid, character.stat.AttackRange, character.tag);
                            EffectManager.Instance.HighlightPointEffect(gridObject.positionOnGrid, character.tag);

                            yield return new WaitForSeconds(2.0f);

                            EffectManager.Instance.HidePointEffect(character.tag);
                            targetGridObject = StageManager.Instance.StageGrid.GetPeripheralPlayer(targetPositionOnGrid);
                            Attack(characterController, characterTurn, targetGridObject);

                            yield return new WaitForSeconds(5.0f);

                            break;
                        }
                    }
                }
            }
        }

        TurnManager.Instance.NextTurn();
    }
}