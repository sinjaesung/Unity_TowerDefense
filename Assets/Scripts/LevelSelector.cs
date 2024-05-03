using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached",1);

        for(int i=0; i<levelButtons.Length; i++)
        {
            if(i+1 > levelReached)//levelReached:3 1,2,3 ��밡�� levelReached:8 1,2,3,4,5,6,7,8 ��밡��
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
   {
        fader.FadeTo(levelName);
   }
}
