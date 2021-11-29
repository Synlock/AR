using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviorHandler : MonoBehaviour
{
    public enum Behaviors { MainMenu, EasyAR1, EasyAR2, Foundation1, Foundation2}

    Button button;
    [SerializeField] UnityEngine.Events.UnityAction behavior;
    [SerializeField] Behaviors behaviorEnum;

    private void Start()
    {
        button = GetComponent<Button>();
        ChooseBehavior();

        button.onClick.AddListener(behavior);
    }

    void ChooseBehavior()
    {
        switch (behaviorEnum)
        {
            case Behaviors.MainMenu:
                behavior = SceneLoader.LoadMainMenu;
                break;
            case Behaviors.Foundation1:
                behavior = SceneLoader.LoadFoundationTask1;
                break;
            case Behaviors.Foundation2:
                behavior = SceneLoader.LoadFoundationTask2;
                break;
            case Behaviors.EasyAR1:
                behavior = SceneLoader.LoadARTask1;
                break;
            case Behaviors.EasyAR2:
                behavior = SceneLoader.LoadARTask2;
                break;
        }
    }
}