namespace GameplayScene.Screens
{
    using GDK.UIManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaScreen : BasePopup
    {
        #region View

        [field: SerializeField]
        public Button ButtonX1 { get; private set; }

        [field: SerializeField]
        public Button ButtonX10 { get; private set; }

        [field: SerializeField]
        public Button ButtonClose { get; private set; }

        [field: SerializeField]
        public Transform ItemListTransform { get; private set; }

        #endregion

        protected override bool IsOverlay => false;

        protected override void OnInit()
        {
            base.OnInit();
            this.ButtonX1.onClick.AddListener(this.OnClickGachaX1);
            this.ButtonX10.onClick.AddListener(this.OnClickGachaX10);
            this.ButtonClose.onClick.AddListener(this.Close);
        }

        protected override void OnShow()
        {
            base.OnShow();

            this.ClearAllItems();
        }

        private void ClearAllItems()
        {
            foreach (Transform child in this.ItemListTransform)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnClickGachaX10()
        {
        }

        private void OnClickGachaX1()
        {
        }
    }
}