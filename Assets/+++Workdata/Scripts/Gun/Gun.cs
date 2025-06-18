using MyBox;
using System;
using Unity.Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject firstProjectileType;
    public GameObject secondaryProjectileType;
    public new ParticleSystem particleSystem;
    [HideInInspector] public float projectileForce;

    public int ammoAmount;
    public float reloadTime;
    //public float shootSpeed;
    [Range(0f,1f)]public float bulletSpread;

    [HideInInspector] public int currentAmmoAmount;
    [HideInInspector] public bool isReloading = false;
}
