using System.Collections;
using UnityEngine;

namespace Amheklerior.Solitaire {

    public class Dealer : MonoBehaviour {

        #region Inspector interface 
            
        [Header("Dependencies:")]
        [SerializeField] private GameObject _cardPrototype;
        //[SerializeField] private DealingDeckStack _dealingStack;
        //[SerializeField] private PlayerColumnStack[] _playerColumns;

        [Space, Header("Animation Settings:")]
        [SerializeField] private float _dealingAnimationTime = 0.05f;

        #endregion
        
        void Awake() {
            if (!_cardPrototype) {
                Debug.LogError("Dealing deck reference is not set.", _cardPrototype);
                throw new MissingReferenceException();
            }
            //CheckDependencies();
            CreateDeck();
        }
        
        void Start() {
            //ClearTable();
            ShuffleDeck();
            //DealCards();
        }
        
        #region Internals

        private Deck _deck;
        
        private void ShuffleDeck() => _deck.Shuffle();
        /*
        private void ClearTable() {
            _dealingStack.Clear();
            foreach (var playerColumn in _playerColumns)
                playerColumn.Clear();
        }

        private void CheckDependencies() {
            if (!_dealingStack) {
                Debug.LogError("Dealing deck reference is not set.", _dealingStack);
                throw new MissingReferenceException();
            }
            if (_playerColumns == null || _playerColumns.Length == 0) {
                Debug.LogError("Player column references are not set.", this);
                throw new MissingReferenceException();
            }
        }
        */
        [ContextMenu("Generate cards")]
        private void CreateDeck() {
            var transform = gameObject.transform;
            _deck = new Deck(_cardPrototype, card => card.transform.parent = transform);
            _deck.GenerateCards();
        }
        /*
        [ContextMenu("Place Deck")]
        private void PlaceDeck() => _dealingStack.PutAll(_deck.Cards);

        [ContextMenu("Deal Cards")]
        private void DealCards() => StartCoroutine(DealCards_Coroutine());

        [ContextMenu("Deal Faced-Down Cards")]
        private void DealFacedDownCards() => StartCoroutine(DealFacedDownCards_Coroutine());

        [ContextMenu("Deal Faced-Up Cards")]
        private void DealFacedUpCards() => StartCoroutine(DealFacedUpCards_Coroutine());

        #region Coroutines 

        private IEnumerator DealCards_Coroutine() {
            PlaceDeck();
            yield return new WaitForSeconds(0.1f);
            DealFacedDownCards();
        }
        
        private IEnumerator DealFacedDownCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            for (int i = 0; i < _playerColumns.Length - 1; i++)
                for (int j = i + 1; j < _playerColumns.Length; j++) {
                    _playerColumns[j].Put(_dealingStack.Take());
                    yield return WaitForAnimationToComplete;
                }
            DealFacedUpCards();
        }

        private IEnumerator DealFacedUpCards_Coroutine() {
            var WaitForAnimationToComplete = new WaitForSeconds(_dealingAnimationTime);
            foreach (var stack in _playerColumns) {
                stack.PlayableCard = _dealingStack.Take();
                stack.PlayableCard.Flip();
                yield return WaitForAnimationToComplete;
            }
        }

        #endregion
        */
        #endregion

    }
}
