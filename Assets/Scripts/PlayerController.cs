using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xInput;
    float moveSpeed = 7;
    float startScale, startPos, endPos, scaleLerp, posLerp;
    float endScale = 3.5f;
    float rightBound = 12;
    float leftBound = -13;
    bool lineBusy = false;
    Hook hookScript;
    BoxCollider2D fishingLineCollider;
    AudioSource sfx;
    public AudioClip catchSound;
    public GameObject fishingLine, hook, hookTarget;

    private void Start()
    {
        startScale = fishingLine.transform.localScale.y;
        startPos = fishingLine.transform.position.y;
        endPos = startPos - 5.5f;
        fishingLineCollider = fishingLine.GetComponent<BoxCollider2D>();
        hookScript = hook.GetComponent<Hook>();
        sfx = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        hook.transform.position = hookTarget.transform.position;
    }
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        if (!GameManager.gameOver)
            transform.Translate(Vector3.right * xInput * moveSpeed * Time.deltaTime);
        SetBounds();
        if (!lineBusy && Input.GetKeyDown(KeyCode.Space) && !GameManager.gameOver)
        {
            StartCoroutine(LowerLine());
            lineBusy = true;
        }
    }
    void SetBounds()
    {
        if (transform.position.x > rightBound)
            transform.position = new Vector3(rightBound, transform.position.y);
        else if (transform.position.x < leftBound)
            transform.position = new Vector3(leftBound, transform.position.y);
    }
    IEnumerator LowerLine()
    {
        const float duration = 3;
        float timeElapsed = 0;
        while (scaleLerp != endScale)
        {
            timeElapsed += Time.deltaTime;
            scaleLerp = Mathf.Lerp(startScale, endScale, timeElapsed / duration);
            fishingLine.transform.localScale = new Vector3(fishingLine.transform.localScale.x, scaleLerp, fishingLine.transform.localScale.z);
            posLerp = Mathf.Lerp(startPos, endPos, timeElapsed / duration);
            fishingLine.transform.position = new Vector3(fishingLine.transform.position.x, posLerp, fishingLine.transform.position.z);
            if (hookScript.objectCaught)
            {
                StartCoroutine(RaiseLine());
                yield break;
            }
            yield return null;
        }
        StartCoroutine(RaiseLine());
    }
    IEnumerator RaiseLine()
    {
        float timeElapsed = 0;
        float currentPos = fishingLine.transform.position.y;
        float currentScale = fishingLine.transform.localScale.y;
        float duration = (currentScale / endScale) * 3;
        /*
        Duration is scaled by the current scale of the hook. The position variable could
        have been used, it didn't matter. I just needed a variable to calculate the percentage
        of where the hook is on the line's path. For example, if the start scale is 1 and end scale is 7.5, a
        currentScale value of 3.75 would mean the hook is 50% of the way down. 
        The duration to lower the line is always 3 seconds, but it has to be scaled when 
        raising because dynamic values are used for the lerp values in this routine.

        If the duration remained constant like in the lowering line routine, then the time 
        to reel in would scale based on how far the line was lowered. It sounds weird, but
        if duration is constant, then the reel in time would not be constant. I need the reel
        in time to be constant so that lowering the line and reeling in feels consistent.

        Update: The reel in duration is still being scaled for some reason, but it wasn't as bad as it was before. I realized that
        making an animation would be much simpler than this god awful lerping routine, but I couldn't figure out the timing, so the lerp stays.
        */
        while (scaleLerp != startScale)
        {
            timeElapsed += Time.deltaTime;
            scaleLerp = Mathf.Lerp(currentScale, startScale, (timeElapsed / duration));
            fishingLine.transform.localScale = new Vector3(fishingLine.transform.localScale.x, scaleLerp, fishingLine.transform.localScale.z);
            posLerp = Mathf.Lerp(currentPos, startPos, (timeElapsed / duration));
            fishingLine.transform.position = new Vector3(fishingLine.transform.position.x, posLerp, fishingLine.transform.position.z);
            yield return null;
        }
        lineBusy = false;
        if (hookScript.objectCaught)
        {
            hookScript.CatchFish();
            sfx.PlayOneShot(catchSound);
        }
    }
}
