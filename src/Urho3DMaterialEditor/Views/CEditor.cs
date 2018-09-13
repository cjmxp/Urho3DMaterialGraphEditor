﻿using ICSharpCode.AvalonEdit;
using System;
using System.ComponentModel;
using System.Windows;

namespace Urho3DMaterialEditor.Views {
    public class MvvmTextEditor : TextEditor, INotifyPropertyChanged {
        public static DependencyProperty DocumentTextProperty =
            DependencyProperty.Register("DocumentText", typeof(string), typeof(MvvmTextEditor),
            new PropertyMetadata((obj, args) => {
                MvvmTextEditor target = (MvvmTextEditor)obj;
                target.DocumentText = (string)args.NewValue;
            })
        );

        public string DocumentText {
            get { return base.Text; }
            set {
       
                var od = base.SelectionStart;
                if (value!=null)base.Document.Text = value;
                base.SelectionStart = od;
    
            }
        }

        public override void BeginInit() {           

            base.BeginInit();
            base.Document.UndoStack.SizeLimit = 1444;

            //base.Document.UndoStack.StartUndoGroup("mse");
        }
        protected override void OnTextChanged(EventArgs e) {
            //RaisePropertyChanged("DocumentText");
            //base.OnTextChanged(e);
            SetCurrentValue(DocumentTextProperty, base.Text);
            base.OnTextChanged(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string info) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }


    }
}
//using FastColoredTextBoxNS;
//using System;
//using System.Text.RegularExpressions;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Forms;
//using System.Windows.Forms.Integration;
//using System.Windows.Media;
//using System.Windows.Threading;

//namespace Urho3DMaterialEditor.Views {

//    public class CEditor : WindowsFormsHost {
//        private readonly FastColoredTextBox _Tbox = new FastColoredTextBox();
//        WindowsFormsHost host = new WindowsFormsHost();

//        public static readonly DependencyProperty TextProperty =
//            DependencyProperty.Register("Text",
//                typeof(string), typeof(CEditor),
//                new PropertyMetadata("", new PropertyChangedCallback(
//            (d, e) => {
//                var textBoxHost = d as CEditor;
//                if (textBoxHost != null && textBoxHost._Tbox != null) {
//                    textBoxHost._Tbox.Text = textBoxHost.GetValue(e.Property) as string;
//                }
//            }), null));

//        public CEditor() {
//            Child = _Tbox;
//            //_Tbox.KeyPressing += _innerTextbox_key;

//            _Tbox.TextChanged += _innerTextbox_TextChanged;
//            _Tbox.Font = new System.Drawing.Font("Consolas", 9);
//            //_Tbox.SyntaxHighlighter = new SyntaxHighlighter() {  "C#" };
//            //_Textbox.ForeColor = System.Drawing.Color.White;
//            synt();

//            //_Tbox.MouseDown += _inner_click;
//            Toe.Scripting.WPF.Views.ScriptView.refr += refresh;

//        }




//private void refresh(float s){
//            _Tbox.Left+=1; _Tbox.Left -= 1;
//        }

//        private void synt() {
//            _Tbox.Language = FastColoredTextBoxNS.Language.CSharp;
//            _Tbox.CommentPrefix = "//";
//            _Tbox.AutoIndentNeeded += fctb_AutoIndentNeeded;
//            //call OnTextChanged for refresh syntax highlighting
//            _Tbox.OnTextChanged();
//        }

//        private void _innerTextbox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e) {
//         if(_Tbox.Text.Contains("iChannel")) _Tbox.Text = _Tbox.Text.Replace("iChannel0", "sDiffMap").Replace("iChannel1", "sSpecMap").Replace("iChannel2", "sNormalMap");
//            SetValue(TextProperty, _Tbox.Text);

//        }

//        public string Text {
//            get { return (string)GetValue(TextProperty); }
//            set {
//                SetValue(TextProperty, value);
//            }
//        }


//        private void fctb_AutoIndentNeeded(object sender, AutoIndentEventArgs args) {
//            //block {}
//            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
//                return;
//            //start of block {}
//            if (Regex.IsMatch(args.LineText, @"^[^""']*\{")) {
//                args.ShiftNextLines = args.TabLength;
//                return;
//            }
//            //end of block {}
//            if (Regex.IsMatch(args.LineText, @"}[^""']*$")) {
//                args.Shift = -args.TabLength;
//                args.ShiftNextLines = -args.TabLength;
//                return;
//            }
//            //label
//            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
//                !Regex.IsMatch(args.LineText, @"^\s*default\s*:")) {
//                args.Shift = -args.TabLength;
//                return;
//            }
//            //some statements: case, default
//            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)")) {
//                args.Shift = -args.TabLength / 2;
//                return;
//            }
//            //is unclosed operator in previous line ?
//            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
//                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
//                {
//                    args.Shift = args.TabLength;
//                    return;
//                }
//        }
//        //styles
//        //TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
//        //TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
//        //TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
//        //TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
//        //TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
//        //TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
//        //TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
//        //MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
//    }
//}