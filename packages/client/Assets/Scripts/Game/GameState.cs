using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GamePhase {Lobby, Build, Action, PostGame,_Count}
public class GameState : MonoBehaviour
{
    public static GamePhase Phase {get{return Instance.phase;}}
    public static GameState Instance;
    public GamePhase phase;
    public PhaseUI [] phaseUI;
    public static System.Action<GamePhase> OnPhaseUpdate;

    void Awake() {
        Instance = this;
        phase = GamePhase.Lobby;
    }

    void Start() {

    }

    public void UpdatePhase(GamePhase newPhase) {

        phaseUI[(int)phase].ToggleWindow(false);

        if(phase == GamePhase.Lobby) {

        } else if(phase == GamePhase.Build) {

        } else if(phase == GamePhase.Action) {

        } else if(phase == GamePhase.PostGame) {

        }

        phase = newPhase;

        phaseUI[(int)newPhase].ToggleWindow(true);

        if(phase == GamePhase.Lobby) {

        } else if(phase == GamePhase.Build) {

        } else if(phase == GamePhase.Action) {

        } else if(phase == GamePhase.PostGame) {

        }

    }

    void NextPhase() {
        UpdatePhase((GamePhase)((int)(phase + 1)%(int)GamePhase._Count));
    }

    void OnDestroy() {
        Instance = null;
    }

    #if UNITY_EDITOR
    [MenuItem("MyMenu/Do Something with a Shortcut Key &q")]
    static void TogglePhase()
    {
        GameState gs = FindObjectOfType<GameState>();

        if(!gs) {return;}

        gs.NextPhase();
    }
    #endif
}
