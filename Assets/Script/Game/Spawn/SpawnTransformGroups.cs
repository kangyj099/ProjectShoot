using UnityEngine;

[System.Serializable]
public struct SpawnTransformGroup
{
    public Color gizmoColor;    // 인스펙터에서 그룹별로 구분하기 위한 색상
    public Transform[] positions;

    public Transform GetRandomTransform()
    {
        // 지정된 포지션 없으면 null 반환
        if (positions == null || positions.Length == 0)
        {

            Debug.LogError($"몬스터 스폰 Transform 그룹에 지정된 위치가 없음");
            return null;
        }

        return positions[Random.Range(0, positions.Length)];
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (var pos in positions)
        {
            if (pos != null)
            {
                Gizmos.DrawSphere(pos.position, 0.5f);
            }
        }
    }
}

public class SpawnTransformGroups : MonoBehaviour
{
    public SpawnTransformGroup[] spawnTransformGroups;
    public int GroupCount => spawnTransformGroups != null ? spawnTransformGroups.Length : 0;

    public Transform GetRandomTransform(int groupIndex)
    {
        if (spawnTransformGroups == null || spawnTransformGroups.Length <= groupIndex)
        {
            Debug.LogError($"몬스터 스폰 위치 그룹 인덱스 {groupIndex}가 유효하지 않음");
            return null;
        }
        return spawnTransformGroups[groupIndex].GetRandomTransform();
    }

    private void Awake()
    {
        // 스폰 위치 그룹이 하나도 없으면 에러 로그 출력
        if (spawnTransformGroups == null || spawnTransformGroups.Length == 0)
        {
            Debug.LogError($"몬스터 스폰 위치 그룹이 하나도 지정되지 않음");
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnTransformGroups != null)
        {
            foreach (var group in spawnTransformGroups)
            {
                group.OnDrawGizmos();
            }
        }
    }
}
