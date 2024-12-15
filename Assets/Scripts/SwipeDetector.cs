using UnityEngine;

public class SwipeDetector : MonoBehaviour
{

    public SoccerGame game;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float swipeThreshold = 5f; // Минимальная длина свайпа в пикселях

    void Update()
    {
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Сохраняем начальную позицию касания
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Сохраняем конечную позицию касания
                    endTouchPosition = touch.position;
                    CheckSwipe();
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    // Игнорируем, ждем завершения свайпа
                    break;
            }
        }
    }

    void CheckSwipe()
    {
        float verticalSwipeDistance = endTouchPosition.y - startTouchPosition.y;

        // Проверяем, достаточно ли длинный свайп
        if (Mathf.Abs(verticalSwipeDistance) >= swipeThreshold)
        {
            if (verticalSwipeDistance > 0)
            {
                Debug.Log("Swipe Up Detected");
                OnSwipeUp();
            }
            else
            {
                Debug.Log("Swipe Down Detected");
                OnSwipeDown();
            }
        }

        // Сброс значений для следующего свайпа
        startTouchPosition = Vector2.zero;
        endTouchPosition = Vector2.zero;
    }
    void OnSwipeUp()
    {
        // Логика для свайпа вверх
        Debug.Log("Perform action for swipe up.");
        game.HeadAttack();
    }

    void OnSwipeDown()
    {
        // Логика для свайпа влево
        Debug.Log("Perform action for swipe left.");
        game.LegAttack();
    }
}
