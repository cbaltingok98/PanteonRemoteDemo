using UnityEngine;

public class Painter : MonoBehaviour {

    public Material hitMaterial;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update ()
    {
        HandlePaint();
    }

    private void HandlePaint()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Ray ray;
            if (_gameManager.IsKeyboard())
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                var touch = Input.GetTouch(0);
                ray = Camera.main.ScreenPointToRay(touch.position);
            }
      
            if (Physics.Raycast(ray, out var hitInfo))
            {
                var rig = hitInfo.collider;
                if (!rig.gameObject.CompareTag("Paintable")) return;
                
                rig.gameObject.tag = "Painted";
                rig.GetComponent<MeshRenderer>().material = hitMaterial;
                rig.GetComponentInParent<PaintWall>().UpdatePercentage();
            }
        }
    }
}