#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using IWorld.ContractDefinition;
using mud.Unity;
using UniRx;
using ObservableExtensions = UniRx.ObservableExtensions;

public class RockMUD : MonoBehaviour
{
    
    public Transform Root;
    public string key;
    private Vector3? _onchainPosition;
    private Vector3 enginePosition;
    private CompositeDisposable _disposers = new();
    Vector3 targetPos;
    public AudioClip [] pushes;
    public AudioClip pushBase;
    public SPAudioSource sfx;
    public ParticleSystem fx;

    float alive = 0f;

    public void Awake() {

        float random = UnityEngine.Random.Range(0f,1f);
        if(random < .25f) {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        } else if(random < .5f) {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }   else if(random < .75f) {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        } else {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }

        // net = NetworkManager.Instance;
        // net.OnNetworkInitialized += Spawn;

        // moveMarker.SetActive(false);
        // _player = GetComponent<PlayerSync>();

        targetPos = transform.position;

        var RockSub  = ObservableExtensions.Subscribe(PositionTable.OnRecordUpdate().ObserveOnMainThread(),
                OnChainPositionUpdate);
        _disposers.Add(RockSub);

    }

    void Update() {
        alive += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 2.5f * Time.deltaTime);
    }

    private void OnChainPositionUpdate(PositionTableUpdate update)
    {
        if (key == null || update.Key != key) return;
        
        // if (_player.IsLocalPlayer()) {
        //     moveMarker.SetActive(false);
        // }

        var currentValue = update.TypedValue.Item1;
        if (currentValue == null) return;
        var x = Convert.ToSingle(currentValue.x);
        var y = Convert.ToSingle(currentValue.y);
        _onchainPosition = new Vector3(x, 0, y);

        Debug.Log("Rock Pos: " + SPHelper.GiveTruncatedHash(update.Key));
        Debug.Log("Rock Pos: " + _onchainPosition.ToString());
		
		CalculateAnimation((Vector3)_onchainPosition);
        targetPos = (Vector3)_onchainPosition;

        if(alive > 1f) {
            sfx.PlaySound(pushBase);
            sfx.PlaySound(pushes);
            fx.Play();
        }
        // transform.position = (Vector3)_destination;
    }

    
    Vector3 ChainPosToEngine(Vector3 chainPos) {
        RaycastHit hit;
		Physics.Raycast(chainPos + Vector3.up * 100f, Vector3.down, out hit, 200f, SPLayers.InvertMaskPlayers,QueryTriggerInteraction.Ignore);
        return hit.point;
    }
	void CalculateAnimation(Vector3 nextPos) {

		nextPos.y = ChainPosToEngine(nextPos).y;
		enginePosition = nextPos;

		var _lookY = _onchainPosition.Value;
		_lookY.y = transform.position.y;

	}


}
