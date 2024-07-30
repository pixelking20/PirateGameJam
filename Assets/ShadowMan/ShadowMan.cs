using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMan : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    Collider[] navigableAreas;
    PlayerController pc;
    [SerializeField]
    ShadowManTrackingArea[] rooms;
    int roomsInvoked = 0;
    Vector3 startingPosition;
    float collapsedSize = .005f;
    Transform visuals;


    private void Awake()
    {
        startingPosition = transform.position;

        visuals = transform.Find("Visuals");

        pc = FindObjectOfType<PlayerController>();

        foreach(ShadowManTrackingArea room in rooms)
        {
            room.onPlayerAreaChange += OnPlayerAreaChange;
        }
        visuals.localScale = new Vector3(1, collapsedSize, 1);
    }
    void OnPlayerAreaChange(bool entering)
    {
        roomsInvoked += entering ? 1 : -1;

        if (roomsInvoked == 1)
        {
            StopAllCoroutines();
            StartCoroutine(CheckPlayerSpaceValidTravel());
        }
        else if (roomsInvoked == 0)
        {
            StopAllCoroutines();
            StartCoroutine(ReturnToStart());
        }
    }
    IEnumerator Rise()
    {
        float totalRange = 1,distanceAlong = visuals.localScale.y;
        float moveTime = 1f, timeElapsed = Mathf.InverseLerp(0,totalRange,distanceAlong) * moveTime;

        while(timeElapsed < moveTime)
        {
            visuals.forward = Vector3.ProjectOnPlane(pc.transform.position - transform.position, transform.up);

            visuals.localScale = Vector3.Lerp(new Vector3(1, collapsedSize, 1), Vector3.one, timeElapsed / moveTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FollowPlayer()
    {
        yield return Rise();

        while (true)
        {
            visuals.forward = Vector3.ProjectOnPlane(pc.transform.position - transform.position, transform.up);
            Move(pc.transform.position);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ReturnToStart()
    {
        yield return Fall();

        while(true)
        {
            if(transform.position != startingPosition)
            {
                visuals.forward = Vector3.ProjectOnPlane(startingPosition - transform.position, transform.up);
                Move(startingPosition);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Fall()
    {
        float totalRange = 1, distanceAlong = 1 - visuals.localScale.y;
        float moveTime = 1f, timeElapsed = Mathf.InverseLerp(0, totalRange, distanceAlong) * moveTime;

        while (timeElapsed < moveTime)
        {
            visuals.forward = Vector3.ProjectOnPlane(pc.transform.position - transform.position, transform.up);

            visuals.localScale = Vector3.Lerp(Vector3.one, new Vector3(1, collapsedSize, 1), timeElapsed / moveTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator CheckPlayerSpaceValidTravel()
    {
        Coroutine activeCoroutine;

        bool following = CheckTrackingSpaceValid(pc.transform.position);

        activeCoroutine = StartCoroutine(following ? FollowPlayer() : ReturnToStart());

        while (true)
        {
            if (!following && CheckTrackingSpaceValid(pc.transform.position))
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = StartCoroutine(FollowPlayer());
                following = true;
            }
            else if(following && !CheckTrackingSpaceValid(pc.transform.position))
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = StartCoroutine(ReturnToStart());
                following = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    bool CheckTrackingSpaceValid(Vector3 goal)
    {
        foreach (Collider c in navigableAreas)
        {
            goal.y = c.transform.position.y;
            if (c.ClosestPoint(goal) == goal)
            {
                return true;
            }
        }
        return false;
    }
    void Move(Vector3 goal)
    {
        Vector3 direction = goal - transform.position;
        direction = Vector3.ProjectOnPlane(direction, transform.up);
        goal = transform.position + direction;
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController pc))
        {
            pc.GetHitByShadowMonster();
        }
    }
}
