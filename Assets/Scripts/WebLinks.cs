using UnityEngine;

public class WebLinks : MonoBehaviour
{
    public void OpenStatesLink() {
        Application.OpenURL("https://en.wikipedia.org/wiki/List_of_states_and_territories_of_the_United_States");
    }
    
    public void OpenCountryLinks() {
        Application.OpenURL("https://en.wikipedia.org/wiki/List_of_sovereign_states");
        
    }

    public void QuitGame() {
        Application.Quit();
    }
}
