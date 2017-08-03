/************************************************************
■UnityでUDPを受信してみる
	http://qiita.com/nenjiru/items/8fa8dfb27f55c0205651
************************************************************/

/********************
********************/
using UnityEngine;
using System.Collections;

/********************
********************/
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/************************************************************
************************************************************/
public class UDPReceive : MonoBehaviour
{
	public int port = 12346;
	static UdpClient udp;
	Thread thread;
	
	private static Mutex mutex;
	
	static float UDP_time;
	static int UDP_x;
	static int UDP_y;

	void Start ()
	{
		/********************
		udp.Client.ReceiveTimeout = 1000; // ms
			no messageでtimeoutすると、Close();しているようだ.
			既定値は、タイムアウト期間が無限を示す 0.
		********************/
		udp = new UdpClient(port);
		// udp.Client.ReceiveTimeout = 1000; // ms
		
		thread = new Thread(new ThreadStart(ThreadMethod));
		thread.Start(); 
		
		mutex = new Mutex(false, "sampleMutex");
	}

	void Update ()
	{
	}

	void OnApplicationQuit()
	{
		/********************
		http://k-suzuki.hateblo.jp/entry/2013/10/03/135844
			thread.Abort()前にcloseしないと、アプリを終了する時に固まる.
		********************/
		udp.Close();
		
		thread.Abort();
	}

	private static void ThreadMethod()
	{
		while(true)
		{
			/********************
			UdpClient.Receive
				https://msdn.microsoft.com/ja-jp/library/system.net.sockets.udpclient.receive(v=vs.110).aspx
					Receive メソッドは、リモート ホストからデータグラムを受信するまでブロックします。 
					よって、oFの時のように、このthreadが、すごい速さでLoopすることはないので、sleep処理は不要.
			********************/
			IPEndPoint remoteEP = null;
			byte[] data = udp.Receive(ref remoteEP);
			
			string text = Encoding.UTF8.GetString(data);
			// Debug.Log(text);
			
			/********************
			■方法: String.Split を使用して文字列を解析する
				https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/strings/how-to-parse-strings-using-string-split
			********************/
			// char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
			char[] delimiterChars = {','};
			string[] param = text.Split(delimiterChars);
			mutex.WaitOne();
				UDP_time = float.Parse(param[0]);
				UDP_x = int.Parse(param[1]);
				UDP_y = int.Parse(param[2]);
			mutex.ReleaseMutex();
		}
	}
	
	void OnGUI()
	{
		/********************
		■[C#] Formatメソッドを利用して桁数を指定して浮動小数点型(float double)変数の値を出力する
			https://www.ipentec.com/document/document.aspx?page=csharp-format-floating-point-type-output-specified-digits
		********************/
		GUI.color = Color.black;
		mutex.WaitOne();
			string label = string.Format("{0:f5}:{1,5}, {2,5}", UDP_time, UDP_x, UDP_y);
		mutex.ReleaseMutex();
		
		GUI.Label(new Rect(0, 40, 200, 30), label);
	}
}

