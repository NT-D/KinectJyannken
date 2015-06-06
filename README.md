# KinectJyannken Online
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


