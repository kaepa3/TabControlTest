using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp7
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Aurora.CustomControl"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Aurora.CustomControl;assembly=Aurora.CustomControl"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:SelectTextBox/>
    ///
    /// </summary>
    public class SelectTextBox : TextBox
    {
        /// <summary>
        /// 選択状態プロパティ
        /// </summary>
        public static readonly DependencyProperty IsChoiceedProperty =
            DependencyProperty.Register("IsChoiced",
                              typeof(bool),
                              typeof(SelectTextBox),
                              new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsChoicedPropertyChanged)));
        /// <summary>
        /// 選択状態変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void IsChoicedPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var o = (SelectTextBox)sender;
            o.IsChoiced = (bool)args.NewValue;
        }

        /// <summary>
        /// 選択状態
        /// </summary>
        public bool IsChoiced
        {
            set { SetValue(IsChoiceedProperty, value); }
            get { return (bool)GetValue(IsChoiceedProperty); }
        }

        /// <summary>
        /// 背景更新用コントロール
        /// </summary>
        ScrollViewer scrollViewer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static SelectTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectTextBox), new FrameworkPropertyMetadata(typeof(SelectTextBox)));
        }


        /// <summary>
        /// テンプレート適用時のイベント処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 前のテンプレートのコントロールの後処理
            if (this.scrollViewer != null)
            {
                this.scrollViewer.MouseLeftButtonDown -= this.ClickTextBox;
            }

            // テンプレートからコントロールの取得
            this.scrollViewer = this.GetTemplateChild("PART_ContentHost") as ScrollViewer;

            // イベントハンドラの登録
            if (this.scrollViewer != null)
            {
                this.scrollViewer.MouseLeftButtonDown += this.ClickTextBox;
            }
        }

        /// <summary>
        /// クリック時の動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickTextBox(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                this.IsChoiced = !this.IsChoiced;
            }
        }
    }
}
