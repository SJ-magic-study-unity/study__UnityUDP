/************************************************************
************************************************************/
#include "ofApp.h"

/************************************************************
************************************************************/
//--------------------------------------------------------------
void ofApp::setup(){
	/********************
	********************/
	ofSetWindowTitle("UDP:receive");
	ofSetVerticalSync(true);
	ofSetFrameRate(30);
	ofSetWindowShape(WIDTH, HEIGHT);
	ofSetEscapeQuitsApp(false);
	
	/********************
	********************/
    //create the socket and bind to port 11999
	udpConnection.Create();
	udpConnection.Bind(12345);
	udpConnection.SetNonBlocking(true);
	
	UDP_ElapsedTime = -1;
	UDP_mouseX = -1;
	UDP_mouseY = -1;
}

//--------------------------------------------------------------
void ofApp::update(){
	char udpMessage[SIZE_UPD];
	udpConnection.Receive(udpMessage, SIZE_UPD);
	
	string message=udpMessage;
	if(message!=""){
		vector<string> param = ofSplitString(message, ",");
		
		UDP_ElapsedTime = atof(param[0].c_str());
		UDP_mouseX = atoi(param[1].c_str());
		UDP_mouseY = atoi(param[2].c_str());
		
		while(message != ""){
			udpConnection.Receive(udpMessage,SIZE_UPD);
			message = udpMessage;
		}
	}
}

//--------------------------------------------------------------
void ofApp::draw(){
	ofBackground(30);
	
	char ch_Message[500];
	sprintf(ch_Message, "%5.1f\n(%5d, %5d)", UDP_ElapsedTime, UDP_mouseX, UDP_mouseY);
	ofSetColor(255);
	ofDrawBitmapString(ch_Message, 10, 20);
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){

}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){ 

}
