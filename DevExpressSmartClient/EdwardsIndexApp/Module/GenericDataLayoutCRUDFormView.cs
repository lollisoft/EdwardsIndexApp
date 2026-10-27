using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI;
using DevExpress.Xpo;

namespace EdwardsIndexAppModule.Forms
{
    [SmartPart]
    public partial class GenericDataLayoutCRUDFormView<T> : DevExpress.XtraEditors.XtraUserControl where T : XPLiteObject
    {
		private int position;
		
		public void Position(int _position) {
			position = _position;
		}

        // The T state is stored in our parent work item
        private T _TItem;

        [State]
        public T TItem
        {
            set
            {
                if (_TItem != null)
                {
                    _TItem.Save();
                }

                _TItem = value;

                if (_TItem == null)
                {
                    TItemBindingSource.DataSource = Activator.CreateInstance<T>();
                    dataLayoutControl1.BeginUpdate();
                    dataLayoutControl1.ForceInitialize();
                    dataLayoutControl1.RetrieveFields();
                    dataLayoutControl1.EndUpdate();
                }
                else 
                {
                    TItemBindingSource.DataSource = _TItem;
                } 
            }
            get
            {
                return _TItem;
            }
        }

        public GenericDataLayoutCRUDFormView()
        {
            InitializeComponent();

            TItemBindingSource = new BindingSource() { 
                DataSource = typeof(T) 
            };

            dataLayoutControl1.DataSource = TItemBindingSource;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Refresh();
        }

        public void Refresh()
        {
            if (_TItem != null)
            {
                TItemBindingSource.DataSource = _TItem;
                dataLayoutControl1.BeginUpdate();
                dataLayoutControl1.ForceInitialize();
                dataLayoutControl1.RetrieveFields();
                dataLayoutControl1.EndUpdate();
            }
        }

		public void EndEdit()
        {
            TItemBindingSource.EndEdit();
        }

        private void FormulareDataLayoutCRUDFormView_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                TItemBindingSource.DataSource = _TItem;
            }
        }
    }
}
