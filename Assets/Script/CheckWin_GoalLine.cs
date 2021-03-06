using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin_GoalLine : MonoBehaviour
 {
    public GameSceneManager gameSceneManager;

    public int levelIndex; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int rank = RankingManager.instance.GameEnd(levelIndex);
            gameSceneManager.StarRating(rank, levelIndex);

            CharacterSelectionManager csm = CharacterSelectionManager.instance;

            if (csm.currentCharacterIndex == 1
                && other.GetComponent<Character>().isDivisible)
                csm.UnlockCharacter(14); // Unlock purple dinosaur

            switch (levelIndex)
            {
                case 3:
                    if (Characters.instance.GetRemovedCharactersNum() < 2)
                        csm.UnlockCharacter(2);  break; // Hole_Lv2       Lava   Theme Clear & passing the hole without falling -> Unlock ghost
                case 5: csm.UnlockCharacter(10); break; // Catapult_Lv1   Field  Theme Clear -> Unlock sheep
                case 6: csm.UnlockCharacter(11); break; // Catapult_Lv2   Field  Theme Clear -> Unlock reindeer
                case 7: csm.UnlockCharacter(8);  break; // Lever_Tutorial Horror Theme Clear -> Unlock mummy
                case 8:
                    if (Lever_MultipleRotate.switchCount <= 5) // Lever_Lv1  Horror Theme Clear & lever using time is less than 5
                         csm.UnlockCharacter(4); break; // Unlock old computer
                case 9:  csm.UnlockCharacter(9); break; // Lever_Lv2      Horror Theme Clear -> Unlock vampire
                case 10: csm.UnlockCharacter(5); break; // Mix_Lv1        Ice    Theme Clear -> Unlock penguin
                case 11: csm.UnlockCharacter(6); break; // Mix_Lv2        Ice    Theme Clear -> Unlock polar bear
            }

            if (levelIndex == 12)
                gameSceneManager.PopLastLevelPanel();
            else
                gameSceneManager.PopUpWinningPanel();

            other.GetComponentInChildren<Animator>().SetBool("isLevelCleared", true);
            Character[] characters = Characters.instance.transform.GetComponentsInChildren<Character>();
            for (int i = 0; i < characters.Length; ++i)
                characters[i].isGameEnd = true; // just to block the character's moving
        }
    }
}
