using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovementArea : MonoBehaviour
{
    public BoxCollider2D AreaCollider { get; private set; }
    [SerializeField] private float boundaryMargin;
    public Color AreaGizmoColor = new(Color.green.r, Color.green.g, Color.green.b, 0.1f);

    private void OnValidate()
    {
        if (AreaCollider == null)
        {
            return;
        }

        if (boundaryMargin >= AreaCollider.size.x / 2
            || boundaryMargin >= AreaCollider.size.y / 2)
        {
            Debug.LogError($"{gameObject.name}:{name} BoundaryMargin값이 영역의 절반 이상으로 큼 ");
            boundaryMargin = Mathf.Min(AreaCollider.size.x / 2, AreaCollider.size.y / 2);
        }
    }

    private void Awake()
    {
        AreaCollider = GetComponent<BoxCollider2D>();
        if (AreaCollider == null)
        {
            Debug.LogError($"{gameObject.name} PlayerArea로 지정된 영역이 없습니다.");
        }
    }

    bool IsOnBorder(Vector3 pos)
    {
        var bounds = AreaCollider.bounds;

        float thickness = 0.5f;

        bool inside = bounds.Contains(pos);
        if (!inside) return false;

        // 경계와의 거리 계산
        float dx = Mathf.Min(pos.x - bounds.min.x, bounds.max.x - pos.x);
        float dz = Mathf.Min(pos.z - bounds.min.z, bounds.max.z - pos.z);

        float minDist = Mathf.Min(dx, dz);

        return minDist <= thickness;
    }

    public Vector3 Clamp(Vector3 position)
    {
        Bounds bounds = AreaCollider.bounds;

        position.x = Mathf.Clamp(position.x,
            bounds.min.x + boundaryMargin,
            bounds.max.x - boundaryMargin);
        position.y = Mathf.Clamp(position.y,
            bounds.min.y + boundaryMargin,
            bounds.max.y - boundaryMargin);

        return position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = AreaGizmoColor;

        if (AreaCollider != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(AreaCollider.bounds.center, AreaCollider.bounds.size);
        }
    }
}
