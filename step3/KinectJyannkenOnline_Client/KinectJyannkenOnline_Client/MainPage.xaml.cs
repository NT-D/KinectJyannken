using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPreview.Kinect;//Kinect用のネームスペース
using System.Diagnostics;//[出力]ウィンドウに情報を出力するために追加
//SignalRでリアルタイム通信をするために追加するネームスペース(Step2で追加)
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace KinectJyannkenOnline_Client
{
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Kinect センサー用の変数
        /// </summary>
        private KinectSensor kinect = null;

        /// <summary>
        /// bodyフレームを取得する Reader のための変数
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// Body のデーターを格納する配列
        /// </summary>
        private Body[] bodies;

        /// <summary>
        /// SignalR用の変数 (Step2で追加)
        /// 変数名は何でもよいが、Web側作成時に参照する資料と沿って進めるために、あえてchatとつけています。
        /// </summary>
        IHubProxy chat;

        public MainPage()
        {
            //SignalRのコネクションを設定します(Step2で追加)
            this.makeConnection();

            //Kinect センサー オブジェクトを取得します。
            this.kinect = KinectSensor.GetDefault();

            //センサーが認識する体の総数分の配列を作成します。
            this.bodies = new Body[this.kinect.BodyFrameSource.BodyCount];

            //body frame 用の Reader を取得します。
            this.bodyFrameReader = this.kinect.BodyFrameSource.OpenReader();

            //body frame 到着時のイベントハンドラを登録
            this.bodyFrameReader.FrameArrived += bodyFrameReader_FrameArrived;

            //Kinect センサーを起動します。
            this.kinect.Open();

            //画面上のコンポーネントを初期化します。
            this.InitializeComponent();
        }

        //bodyframe が到着した際のイベントハンドラ
        void bodyFrameReader_FrameArrived(BodyFrameReader sender, BodyFrameArrivedEventArgs args)
        {
            //bodyframe への参照から実際のデータを取得します。
            using (BodyFrame bodyframe = args.FrameReference.AcquireFrame())
            {
                if (bodyframe != null)
                {
                    //保持するデータを最新のものに更新する
                    bodyframe.GetAndRefreshBodyData(this.bodies);

                    //グー、チョキ、パー情報をコンソールに出力するメソッドを呼び出します。
                    this.writeJyannkenState();
                }
            }
        }

        //手のグー、チョキ、パー状態をコンソールに出力するメソッドです。
        private void writeJyannkenState()
        {
            int bodycount = 1;
            //複数の体をトラックしている際に、一つ一つの体の情報をトラックします。
            foreach (Body body in bodies)
            {
                //体が正常にトラックされている場合には次の処理に進みます。
                if (body.IsTracked)
                {
                    //複数の体がトラックされている際に、何番目の体かを表示します。
                    //同じ体を追跡したい場合には、bodies[<追跡したい番号>]のようにインデックスを使ってアクセスする必要があります。
                    //Debug.WriteLine(bodycount.ToString() + "番目の体");
                    bodycount++;

                    //右手の状態を表します。
                    //https://msdn.microsoft.com/en-us/library/windowspreview.kinect.handstate.aspx
                    Debug.WriteLine("右手の状態" + body.HandRightState);

                    //右手の状態に合わせてグー、チョキ、パーを[出力]画面に出力します。
                    switch (body.HandRightState)
                    {
                        case HandState.Closed:
                            Debug.WriteLine("グー");
                            //SignalRでじゃんけんデータを送信します。コードの分かりやすさ優先のため、それぞれの条件分岐内でデータを送信。
                            this.sendMessage(body.HandRightState.ToString());
                            break;
                        case HandState.Lasso:
                            Debug.WriteLine("チョキ");
                            //SignalRでじゃんけんデータを送信します。
                            this.sendMessage(body.HandRightState.ToString());
                            break;
                        case HandState.Open:
                            Debug.WriteLine("パー");
                            //SignalRでじゃんけんデータを送信します。
                            this.sendMessage(body.HandRightState.ToString());
                            break;
                        default:
                            Debug.WriteLine("未検知もしくは状態が取得できていない");
                            break;
                    }
                }
            }
        }

        #region SignalRリアルタイム通信用の関数(Step2で追加)
        /// <summary>
        /// Web側とのコネクションを確立するためのメソッド
        /// </summary>
        async private void makeConnection()
        {
            try
            {
                var hubConnection = new HubConnection("通信先のURLを入力してください。");
                /*
                 * 下記のように自分がリクエストをするサーバーのURLを記載する。
                 * var hubConnection = new HubConnection("http://localhost:3114/");
                 * ローカルホストでもOKですし、http://test.azurewebsites.net/　のようなWebサーバーでもOKです。
                 * */
                chat = hubConnection.CreateHubProxy("ChatHub");
                await hubConnection.Start();
                await chat.Invoke("Notify", "Kinect", hubConnection.ConnectionId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// メッセージを送信するためのメソッド
        /// </summary>
        /// <param name="message"></param>
        async private void sendMessage(string message)
        {
            try
            {
                await chat.Invoke("Send", "kinect", message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
