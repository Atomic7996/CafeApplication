﻿#pragma checksum "..\..\..\Pages\PageAddEditOrderCombos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7DD788C67665C8F0DB6DE535097A40261814C4CE392C067B5B9736B5948E8F19"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CafeApplication.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CafeApplication.Pages {
    
    
    /// <summary>
    /// PageAddEditOrderCombos
    /// </summary>
    public partial class PageAddEditOrderCombos : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbFindFoodStaff;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbAllCombos;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGetCombo;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelCombo;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbSelectedCombos;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run tbFullPrice;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CafeApplication;component/pages/pageaddeditordercombos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tbFindFoodStaff = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.tbFindFoodStaff.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbFindCombos_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.btnClear.Click += new System.Windows.RoutedEventHandler(this.btnClear_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbAllCombos = ((System.Windows.Controls.ListBox)(target));
            
            #line 49 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.lbAllCombos.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lbAllCombos_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.lbAllCombos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbAllProducts_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnGetCombo = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.btnGetCombo.Click += new System.Windows.RoutedEventHandler(this.btnGetCombo_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCancelCombo = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.btnCancelCombo.Click += new System.Windows.RoutedEventHandler(this.btnCancelCombo_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lbSelectedCombos = ((System.Windows.Controls.ListBox)(target));
            
            #line 99 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.lbSelectedCombos.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lbSelectedCombos_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 99 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.lbSelectedCombos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbSelectedCombos_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbFullPrice = ((System.Windows.Documents.Run)(target));
            return;
            case 8:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\..\Pages\PageAddEditOrderCombos.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

