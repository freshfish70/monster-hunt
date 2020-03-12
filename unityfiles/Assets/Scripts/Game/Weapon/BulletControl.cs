﻿using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO: maybe blop this in an interface like the HC/enemy relation
*/

public class BulletControl : MonoBehaviour {


    private BulletData bulletData;
    private BulletData currentBuildBulletData = null;
    private IEnumerator activeEnumerator;




    // -- properties -- //

    internal BulletData BulletData {
        get{
            if (bulletData == null){
                throw new System.Exception("Bullet data missing");
            }
            return bulletData;
        }
        
        set{
            this.bulletData = value;
            this.BuildBullet();
        }
    }

    private void BuildBullet(){
        if (this.currentBuildBulletData != this.bulletData){
            
            SpriteRenderer spriteRender = null;
            BoxCollider2D boxCol = null;
            Rigidbody2D rigidB2d = null;

            

            

            this.gameObject.transform.localScale = this.bulletData.SpriteTransform.lossyScale;
      


            if (!this.gameObject.TryGetComponent(out spriteRender)) {
                spriteRender = this.gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
            } 
            spriteRender.sprite = this.bulletData.Sprite;


            if (!this.gameObject.TryGetComponent(out rigidB2d)) {
                rigidB2d = this.gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
            } 
            rigidB2d.bodyType = RigidbodyType2D.Kinematic;

            if (!this.gameObject.TryGetComponent(out boxCol)) {
                boxCol = this.gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            }
            boxCol.isTrigger = true;
            boxCol.size = spriteRender.sprite.bounds.size;
            
            
            this.gameObject.SetActive(false);
            currentBuildBulletData = bulletData;
        }
        
    }

    /// <summary>
    /// Starts the time to live timer
    /// </summary>
    private void StartTtlTimer() {
        this.activeEnumerator = TtlTimer(this.BulletData.TimeToLive);
        StartCoroutine(this.activeEnumerator);
    }


    private IEnumerator TtlTimer(int waitTime){
        yield return new WaitForSeconds(waitTime);
        this.DisableSelf();      
    }

    /// <summary>
    /// Disables the game object so it can be 
    /// collected in the local gun controller
    /// </summary>
    private void DisableSelf() {
        this.gameObject.SetActive(false);
        StopCoroutine(this.activeEnumerator);
    }

    

    // -- unity -- //
    void Update() {
        Vector2 localVelocity = new Vector2(this.BulletData.Velocity, 0) * Time.deltaTime;
        this.gameObject.transform.Translate(localVelocity.x, localVelocity.y, 0);
    }

    void OnTriggerEnter2D(Collider2D Col) {
        if (!Col.TryGetComponent(out PlayerHealthController ph)) {
            if (Col.TryGetComponent(out EnemyHealthController enemyHealth)) {
                enemyHealth.ApplyDamage(this.BulletData.Damage);
            }
        }

        if (!Col.TryGetComponent(out BulletControl _) && !Col.TryGetComponent(out PlayerHealthController a)){
            this.DisableSelf();
        }
        
    }

    void OnDestroy() {
        if (this.activeEnumerator != null){
            StopCoroutine(this.activeEnumerator);
        }
    }

    void OnEnable() { 
        this.StartTtlTimer();
    } 

}