## Android Vibration
-unity에서 android vibration관련 설정에 필요한 파일이다  

## Usage
-해당 프로젝트 폴더의 Asset\Plugins\Android에 넣는다.    
-Vibration.cs를 사용한다.  

## Notice
- AndroidManifest.xml 추출하는 방법 (2021 3.14f1 버전)  
1. 해당 프로젝트의 Project Settings → Player → Android → Publishing Settings에 가서 Build 부분에 Custom Main Manifest를 활성화 한다.  
2. Android로 build target을 바뀐 뒤, build한다. (빌드 경로 상관 없음)  
3. 빌드 후 프로젝트 Assets\Plugins\Android 폴더 내 AndroidManifest.xml이 생성되는거 확인  
