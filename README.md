# KinectJyannken Online 概要
KinectJyannken Online は3つの要素を学びながら、Kinectで遊べるサンプルプロジェクトです。

1. Kinect での手の状態検知(グー、チョキ、パー)の実装方法
2. Windows ストアアプリから Web アプリへのリアルタイム通信実装方法。リアルタイム通信には[SignalR](https://github.com/SignalR/SignalR)という技術を使っています。
3. Web アプリ側の実装

説明はリクエストがあれば順次追加していきますが、まずは基本的な流れをここに記載していきます。


## システム要件
* Windows 8 以上の開発用PC。PCの詳細な要件は[こちら](https://www.microsoft.com/en-us/download/details.aspx?id=44561)を参照。
* 開発用PCに[Kinect for Windows SDK 2.0](https://www.microsoft.com/en-us/download/details.aspx?id=44561)がインストールされている
* Kinect v2.0センサー。[マイクロソフトストア](http://www.microsoftstore.com/store/msjp/ja_JP/home)で購入するなどして入手してください。
* Microsoft Azure のサブスクリプション
* *　無償版の入手は[こちら](http://azure.microsoft.com/ja-jp/pricing/free-trial/)から。
* Visual Studio 2013
* * インストール方法などは[こちら](http://nt-d.hatenablog.com/entry/2014/09/02/002216)を参照してください。

## サンプルの実行画面
### サンプルプロジェクトのダウンロード
1. [Download ZIP]でプロジェクトファイルをダウンロードします。
2. ZIP ファイルを展開します。
3. "\KinectJyannken-master\KinectJyannken-master\step3\KinectJyannkenOnline_Web\KinectJyannkenOnline_Web.sln"をダブルクリックして開きます。
4. Visual Studio の画面が表示されます。

### Azure Web Apps へのプロジェクトの発行
1. 画面右手の[ソリューション エクスプローラー]ウィンドウにある"KinectJyannkenOnline_Web"プロジェクトを右クリックして、[発行]をクリックします。
2. [Webを発行]画面が表示されるので、[Microsoft Azure Web Apps]を選択します。
3. [Select Existing Web Apps]画面が表示されるので、Azure サブスクリプションでサインイン後、[Existing Web Apps]ドロップダウンボックスで既存の Web Apps を選択するか、[新規]で新しい Web Apps を選択後、[OK]ボタンをクリックします。
4. [Webを発行]画面に戻るので、[発行]ボタンをクリックします。
5. ブラウザが起動し、[Kinectじゃんけんオンライン]ページが表示されます。このページの URL をメモしてください。この後の手順で使用します。
これで、Kinect アプリから受信した情報を表示するための Webページの準備が整いました。

### Kinect アプリの実行
1. Kinect をPCにつなぎます。
2. "KinectJyannken-master\KinectJyannken-master\step3\KinectJyannkenOnline_Client\KinectJyannkenOnline_Client.sln"をダブルクリックして開きます。
3. Visual Studio の画面が表示されます。
4. 画面右側の[ソリューションエクスプローラー]ウィンドウで"MainPage.xaml.cs"ファイルをダブルクリックします。
5. 画面中央のコーディング画面で、140行目を確認します。行番号が表示されていない方は、[こちら](https://msdn.microsoft.com/ja-jp/library/ms165340.aspx)の情報を参照してください。
6. "var hubConnection = new HubConnection("通信先のURLを入力してください。");"というコードがあるため、[Azure Web Apps へのプロジェクトの発行]でメモした URL を使い、"var hubConnection = new HubConnection("http://kinectcamptest.azurewebsites.net/");" のように書き換えます。
7. [F5]キーを押して、アプリケーションを起動させます。
8. Kinectの前でグー、チョキ、パーをすると、[Azure Web Apps へのプロジェクトの発行]でブラウザで表示されたページに"グー","チョキ","パー"と表示されます。

これで、下記3つの要素技術を使ったサンプルを動作させることができました。

1. Kinect での手の状態検知(グー、チョキ、パー)の実装方法
2. Windows ストアアプリから Web アプリへのリアルタイム通信実装方法。リアルタイム通信には[SignalR](https://github.com/SignalR/SignalR)という技術を使っています。
3. Web アプリ側の実装

#　サンプル解説
## Step1
coming soon

## Step2
coming soon

## Step3
coming soon


