# UnityTest

## 初始化
1. Scene.当前用的EntryScene. 需要将其它scene remove/unload之后可以运行.  
entryScene load后也并没有任何资源的预览，需要运行才可以看见内部内容.  
~~2. item -> 轿子 MainHeroSprite -> Player.~~

## 操作说明
Player1   
方向  WASD   
放下  Q  
拾起/放下  E
跑    R

~~Player2
方向 小键盘8456  
放下  7  
拾起  9  
跑    1  ~~

## 代码
### 1.事件管理 EventManager
1. 在需要注册的object事件脚本中加入事件响应的函数func
2. 在Enums.cs的Event中加入事件的枚举名.
3. 在相应的object脚本中调用 registerEvent，参数是2中的枚举名和1中的回掉函数.
4. 目前并不支持具有argumnet的事件.
