using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterController : MonoBehaviour
{
    GridObject gridObject;
    Character character;
    CharacterAnimator characterAnimator;

    List<Vector3> pathWorldPositions;

    string stepSound;
    string attackSound;
    string skillSound;
    bool isPlayer;

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        character = GetComponent<Character>();
        characterAnimator = GetComponent<CharacterAnimator>();
        stepSound = character.stepSoundType.ToString();
        attackSound = character.attackSoundType.ToString();
        skillSound = character.skillSoundType.ToString();
        isPlayer = gridObject.CompareTag("Player");
    }

    private void RotateCharacter(Vector3 originPosition, Vector3 destinationPosition)
    {
        Vector3 direction = (destinationPosition - originPosition).normalized;
        direction.y = 0;
        transform.DOLookAt(transform.position + direction, 0.25f).SetEase(Ease.InOutSine);
    }

    private void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.DOLookAt(transform.position + direction, 0.15f).SetEase(Ease.InOutSine);
    }

    private void StartBattle()
    {
        if (isPlayer)
        {
            TurnManager.Instance.DisableEndTurnButton();
        }

        CameraManager.Instance.StartBattle(gridObject.gameObject);
    }

    private void EndBattle()
    {
        if (isPlayer)
        {
            TurnManager.Instance.EnableEndTurnButton();
        }

        TurnManager.Instance.CheckEndTurn();
        CameraManager.Instance.EndBattle();
    }

    public void Move(List<PathNode> path)
    {
        StartBattle();

        pathWorldPositions = StageManager.Instance.StageGrid.ConvertPathNodesToWorldPositions(path);

        StageManager.Instance.StageGrid.RemoveObject(gridObject.positionOnGrid, gridObject);

        gridObject.positionOnGrid.x = path[path.Count - 1].posX;
        gridObject.positionOnGrid.y = path[path.Count - 1].posY;

        StageManager.Instance.StageGrid.InvertPassable(gridObject.positionOnGrid);

        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        characterAnimator.StartMoving();

        for (int i = 0; i < pathWorldPositions.Count; i++)
        {
            RotateCharacter(transform.position, pathWorldPositions[i]);
            Managers.Sound.Play(stepSound, SoundType.Effect);
            yield return transform.DOMove(pathWorldPositions[i], 1f).SetEase(Ease.Linear).WaitForCompletion();
        }
        Managers.Sound.Play(stepSound, SoundType.Effect);

        characterAnimator.StopMoving();

        StageManager.Instance.StageGrid.PlaceObject(gridObject.positionOnGrid, gridObject);

        EndBattle();
    }

    public void Attack(GridObject targetGridObject)
    {
        StartBattle();
        StartCoroutine(Attacking(targetGridObject));
    }

    private IEnumerator Attacking(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);

        yield return new WaitForSeconds(1.0f);

        characterAnimator.Attack();
        Managers.Sound.Play(attackSound, SoundType.Effect);

        yield return new WaitForSeconds(0.1f);

        Character target = targetGridObject.GetComponent<Character>();
        int damage = character.GetDamage();

        if (Random.value <= character.stat.CriticalChance)
        {
            damage = (int)(damage * character.stat.CriticalDamageRatio);
        }

        damage = Mathf.Max(damage - target.GetDefense(character.stat.DamageType), 0);
        target.TakeDamage(damage);
        EffectManager.Instance.PlayDamageEffect(targetGridObject.transform.position, damage.ToString(), character.tag);

        yield return new WaitForSeconds(1.0f);

        EndBattle();
    }

    public void Skill(GridObject targetGridObject)
    {
        StartBattle();
        StartCoroutine(Skilling(targetGridObject));
    }

    private IEnumerator Skilling(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);

        yield return new WaitForSeconds(1.0f);

        characterAnimator.Attack();
        character.UseMp();

        EffectManager.Instance.PlaySkillEffect(character.transform.position, targetGridObject.transform.position, character.stat.SkillType);
        Managers.Sound.Play(skillSound, SoundType.Effect);

        yield return new WaitForSeconds(0.1f);

        Character target = targetGridObject.GetComponent<Character>();
        int damage = character.GetDamage() * character.stat.SkillDamageRatio;
        damage = Mathf.Max(damage - target.GetDefense(character.stat.DamageType), 0);
        target.TakeDamage(damage);
        EffectManager.Instance.PlayDamageEffect(targetGridObject.transform.position, damage.ToString(), character.tag);

        yield return new WaitForSeconds(1.0f);

        EndBattle();
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}