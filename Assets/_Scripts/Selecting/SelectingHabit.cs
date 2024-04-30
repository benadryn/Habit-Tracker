using UnityEngine;

public class SelectingHabit : MonoBehaviour
{
    private bool _isHeld;
    private bool _isSelected;
    private float _touchStartTime;
    [SerializeField] private float heldThreshold = 1.0f;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchPos = _camera.ScreenToWorldPoint(touch.position);

            var hit = Physics2D.Raycast(touchPos, Vector2.zero);
            if (hit.collider && hit.collider.gameObject == gameObject)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchStartTime = Time.time;
                        _isHeld = false;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:

                        if (Time.time - _touchStartTime >= heldThreshold && hit && !_isHeld)
                        {
                            _isHeld = true;
                            HandleHeld(hit.collider.gameObject);
                        }

                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isHeld = false;
                        break;
                }
            }
        }
    }
    private void HandleHeld(GameObject obj)
    {
        HabitManager.Instance.CheckSelected(obj);
    }
}