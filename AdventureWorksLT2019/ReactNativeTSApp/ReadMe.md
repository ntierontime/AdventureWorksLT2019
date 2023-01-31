npx react-native init MyApp --template react-native-template-typescript
npm run android --deviceId=pixel_3_xl_-_api_28
npm run android --deviceId=pixel_5_-_api_32
npm run android --deviceId=pixel_5_-_api_32_1

%ANDROID_HOME%/tools/emulator -list-avds

npm install -g react-devtools
react-devtools ? How to use it.

https://reactnative.dev/docs/navigation

npm install @react-navigation/native @react-navigation/native-stack
npm install react-native-screens react-native-safe-area-context
ios specific navigation installation: Cocoapods https://cocoapods.org/ Then install the pods
cd ios
pod install
cd ..

