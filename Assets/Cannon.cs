using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Cannon : MonoBehaviour
{
    VisualEffect chargeEffect;
    AudioSource changeAudioSource;
    VisualEffect beamEffect;
    int chargeCount = 0;
    bool beamOn = false;
    AudioSource beamAudioSource;
    float beamTime = 0;

    void Start()
    {
        var c = this.gameObject.transform.Find("ChargeEffect");
        if (c) {
          var eff = c.GetComponent<VisualEffect>();
          this.chargeEffect = eff;
          var aud = c.GetComponent<AudioSource>();
          this.changeAudioSource = aud;
        }
        var b = this.gameObject.transform.Find("BeamEffect");
        if (b) {
          var eff = b.GetComponent<VisualEffect>();
          this.beamEffect = eff;
          this.beamEffect.SetFloat("SpawnRate", 0);
          this.beamAudioSource = b.GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G)) {
          if (chargeCount < 100) {
            chargeCount += 1;
            this.changeAudioSource.Play();
          }
        } else {
          this.changeAudioSource.Stop();
          if (chargeCount == 100) {
            beamOn = true;
            beamAudioSource.Play();
          }
          chargeCount = 0;
        }
        this.chargeEffect.SetFloat("SpawnRate", chargeCount);

        if (beamOn) {
          beamTime += Time.deltaTime;
          this.beamEffect.SetFloat("SpawnRate", 100);

          if (beamTime > 1) {
            beamOn = false;
            beamTime = 0;
            this.beamEffect.SetFloat("SpawnRate", 0);
          } 
        }
    }
}
