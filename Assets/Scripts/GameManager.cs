using System.Collections;
using System.Collections.Generic;
using ThirdPersonCombat.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThirdPersonCombat.Core {

    public class GameManager : MonoBehaviour {
        [SerializeField] string uiSceneName;
        [SerializeField] PlayerStateMachine player;

        private void Awake() {
            SceneManager.LoadScene(uiSceneName, LoadSceneMode.Additive);
        }

        private void Start() {
            UIManager.Instance.SetPlayer(player.Health);
        }
    }

}
