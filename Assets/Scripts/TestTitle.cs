using UnityEngine;

public class TestTitle : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadingManager.LoadScene("Game");
        }
    }
}
