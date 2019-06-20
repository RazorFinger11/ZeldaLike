using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float daySeconds;
    float radDay;
    public float daySpeed = 1;
    Light sunlight;
    float intensity;
    public Gradient ambient;
    public delegate void SunEvent();
    public SunEvent DawnCall;
    public SunEvent DuskCall;

    public static DayNight instance;
    bool day = false;

    private GameObject[] lightArray;
    private float[] lightIntensities;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        lightArray = GameObject.FindGameObjectsWithTag("ActivatableLight");
        lightIntensities = new float[lightArray.Length];

        sunlight = GetComponent<Light>();
        DawnCall += Afternon;
        DuskCall += Morning;
    }
    //86400
    // Update is called once per frame
    void Update()
    {
        daySeconds += Time.deltaTime * daySpeed;
        radDay = daySeconds / (86400 / 2);
        transform.localRotation = Quaternion.Euler(radDay * (150 / Mathf.PI), 0, 0);

        if (intensity > 0.4f)
        {
            intensity = Mathf.Clamp01(Vector3.Dot(transform.forward, Vector3.down) + 0.3f);
        }
        else
        {
            intensity = Mathf.Clamp01(Vector3.Dot(-transform.forward, Vector3.up) - 0.3f);
        }
        sunlight.intensity = intensity;

        RenderSettings.ambientLight = ambient.Evaluate(intensity);

        RenderSettings.fogDensity = 0.001f * intensity;
        if (intensity > 0.4f && !day)
        {
            DuskCall();
            day = true;
        }
        if (intensity < 0.4f && day)
        {
            DawnCall();
            day = false;
        }

    }
    void Morning()
    {
        for(int i = 0; i < lightArray.Length; i++)
        {
            lightIntensities[i] = lightArray[i].GetComponent<Light>().intensity; 
            lightArray[i].GetComponent<Light>().intensity = 0;
            lightArray[i].GetComponentInParent<Renderer>().materials[1].DisableKeyword("_EMISSION");
        }
    }
    void Afternon()
    {
        for (int i = 0; i < lightArray.Length; i++)
        {
            lightArray[i].GetComponent<Light>().intensity = lightIntensities[1];
            lightArray[i].GetComponentInParent<Renderer>().materials[1].EnableKeyword("_EMISSION");
        }
    }
}
