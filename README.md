# RPG-ChatGPT-Demo
A proof-of-concept implementation of an RPG demo game with ChatGPT-based NPCS.

## 1. Setup
1. Open `Assets/GameSetting.asset` in Unity and fill in your OpenAI api key.  
2. Start the game and use WASD and mouse to control the character to get close to other NPCs, and then try click whitespace or mouse left button on dialog options to communicate with them.

## 2. Edit your own npc
See the sample prefab `Assets/Prefabs/Npc.prefab`, all you have to do is replace the fields `Pretrained Messages`, `Initial Communication Content`, `Option Communication Content`. Like what you do on ChatGPT playground, try to find a sequence of dialogs to make ChatGPT response you a corresonding sentence when you tell it a specific keyword.  

For example below, I asked ChatGPT to act as a vendor with some settings and found it work perfectly with my keywords, so next step I filled the dialog into prefab setting. About `Pretrained Messages`, what you talked to ChatGPT is with the role tag `user` and what ChatGPT responsed you is with the role tag `assistant`.  
![ChatGPTVendorExperimentToPrefabSetting](https://github.com/chiaohao/RPG-ChatGPT-Demo/blob/main/README%20Materials/ChatGPTVendorExperimentToPrefabSetting.png)
  
