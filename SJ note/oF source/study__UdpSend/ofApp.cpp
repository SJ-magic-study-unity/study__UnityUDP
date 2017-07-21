/************************************************************
************************************************************/
#include "ofApp.h"

/************************************************************
************************************************************/

//--------------------------------------------------------------
void ofApp::setup(){
	/********************
	********************/
	ofSetWindowTitle("UDP:send");
	ofSetVerticalSync(true);
	ofSetFrameRate(30);
	ofSetWindowShape(WIDTH, HEIGHT);
	ofSetEscapeQuitsApp(false);
	
	/********************
	********************/
    //create the socket and set to send to 127.0.0.1:11999
	udpConnection.Create();
	udpConnection.Connect("127.0.0.1",12346);
	udpConnection.SetNonBlocking(true);
	
	State = STATE_SLEEP;
}

//--------------------------------------------------------------
void ofApp::update(){
	if(State == STATE_SENDING){
		string message="";
		
		/********************
		window focus外れると、mouseX, Yは、更新されなくなるが、program自体は動いている。
		例えば、ofGetElapsedTimef()は更新され続けていて、これをsendし続ける.
		********************/
		message += ofToString(ofGetElapsedTimef()) + "," + ofToString(mouseX) + "," + ofToString(mouseY);
		
		udpConnection.Send(message.c_str(), message.length());
	}
}

//--------------------------------------------------------------
void ofApp::draw(){
	ofBackground(30);
	
	ofSetColor(255);
	if(State == STATE_SLEEP){
		ofDrawBitmapString("SLEEP", 10, 20);
	}else if(State == STATE_SENDING){
		ofDrawBitmapString("SENDING", 10, 20);
	}
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){
	switch(key){
		case 's':
			State = STATE_SLEEP;
			break;
			
		case ' ':
			State = STATE_SENDING;
			break;
	}
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
