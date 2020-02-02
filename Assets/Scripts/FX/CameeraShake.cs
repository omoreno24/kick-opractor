using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameeraShake : MonoBehaviour {
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;
	[Range(0.0f,1.0f)]
	public float camRecoveryTime = 0.3f;

	public float decreaseFactor = 1.0f;

	// How long the object should shake for.
	private float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount = 0.7f;

	private Vector3 shakeDirection = Vector3.one;

    public GameObject Player1;
    public Button ButtonEvent;

    Vector3 originalPos;

    void Start()
    {
        if (camTransform == null)
        {
			camTransform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

	IEnumerator _Shake()
	{
		while (shake > 0)
		{	
			//if the shake isn't for a spesific axis
			if (shakeDirection == Vector3.one)
				camTransform.localPosition = originalPos + (Random.insideUnitSphere * shakeAmount);
			else
				camTransform.localPosition = originalPos + (shakeAmount * shakeDirection);
			
			shake -= Time.deltaTime * decreaseFactor;
			yield return new WaitForEndOfFrame();
		}
		StartCoroutine (ShakeOut());
		shakeDirection = Vector3.one;
	}

	IEnumerator ShakeOut(){
		
		float t = camRecoveryTime;

		while (t > 0) {
			shake = 0f;
			camTransform.localPosition = Vector3.Lerp (camTransform.localPosition, originalPos, 1 - (t / camRecoveryTime));
			t -= 1 * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	public void ShakeOneShot(float ammout = 0.7f)
	{
		Shake (1 * Time.deltaTime, ammout);
	}

    public void ShakeOneShotDirectional(Vector3 direction,float ammount = 0.2f)
	{
		ShakeDirectional (1 * Time.deltaTime, direction, ammount);
	}

	public void Shake(float t, float amount = 0.7f)
    {
		this.shakeAmount = amount;
        this.shake = t;

		StopCoroutine ("_Shake");
		StartCoroutine(_Shake ());
    }

    public void ShakeDirectional(float t, Vector3 direction, float amount = 0.7f)
	{
        shakeDirection = direction;

		Shake (t, amount);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Player1.activeSelf)
        {
            ButtonEvent.onClick.Invoke();
        }
    }
}