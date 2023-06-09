using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterMember
{
    public Character character;
    public CharacterTurn characterTurn;

    public CharacterMember(Character character, CharacterTurn characterTurn)
    {
        this.character = character;
        this.characterTurn = characterTurn;
    }
}

public class CharacterContainer : MonoBehaviour
{
    public CharacterType characterType;
    public List<CharacterMember> characters;

    public void Add(CharacterTurn characterTurn)
    {
        if (characters == null)
        {
            characters = new List<CharacterMember>();
        }

        characters.Add(new CharacterMember(characterTurn.GetComponent<Character>(), characterTurn));
        characterTurn.transform.SetParent(transform);
    }

    public void ResetTurn()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].characterTurn.ResetTurn();
        }
    }

    public bool CheckEndTurn()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (!characters[i].characterTurn.CheckEndTurn())
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckAllDead()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (!characters[i].character.isDead)
            {
                return false;
            }
        }

        return true;
    }
}