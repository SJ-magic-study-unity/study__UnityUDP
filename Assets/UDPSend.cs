/************************************************************
■UnityでUDPを送信してみる
	http://qiita.com/nenjiru/items/d9c4e8a22601deb0425b

■[C#] string(文字列)からバイト型配列 byte[] (バイナリ) に変換する
	https://www.ipentec.com/document/document.aspx?page=csharp-string-to-bytearray

■string.Formatで書式指定
	http://www.kisoplus.com/sample2/sub/format.html
	
■string.Format : 書式指定など
	http://www.atmarkit.co.jp/ait/articles/0401/30/news069.html
************************************************************/

/********************
********************/
using UnityEngine;
using System.Collections;

/********************
********************/
using System.Net.Sockets;
using System.Text;


/************************************************************
************************************************************/
public class UDPSend : MonoBehaviour {
	public string host = "127.0.0.1";
	public int port = 12345;
	private UdpClient client;
	
	enum STATE{
		SLEEP,
		SENDING,
	};
	private STATE State = STATE.SLEEP;

	void Start ()
	{
		client = new UdpClient();
		client.Connect(host, port);
	}

	void Update ()
	{
		/********************
		********************/
		if(Input.GetKeyDown(KeyCode.S)){
			switch(State){
				case STATE.SLEEP:
					State = STATE.SENDING;
					break;
					
				case STATE.SENDING:
					State = STATE.SLEEP;
					break;
			}
		}

		/********************
		********************/
		if(State == STATE.SENDING){
			/*
			byte[] dgram = Encoding.UTF8.GetBytes("10.1,2,3");
			client.Send(dgram, dgram.Length);
			*/
			
			/********************
			********************/
			string message = string.Format("{0},{1},{2}", Time.time/* float sec:ElapsedTime */, Input.mousePosition.x, Input.mousePosition.y);
			
			byte[] dgram = System.Text.Encoding.UTF8.GetBytes(message);
			
			try{
				client.Send(dgram, dgram.Length);
				
			}catch(SocketException){
				/********************
				UdpClient.Sendの投げる例外
					https://msdn.microsoft.com/ja-jp/library/82dxxas0(v=vs.110).aspx
				********************/
				Debug.Log("Error in accessing to Socket. I guess : No client??");
				State = STATE.SLEEP;
			}
		}
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		string label = "";
		if(State == STATE.SLEEP){
			label += "SLEEP(press 's')";
		}else{
			label += "SENDING(press 's')";
		}
		
		GUI.Label(new Rect(0, 15, 200, 30), label);
	}

	void OnApplicationQuit()
	{
		client.Close();
	}
}
