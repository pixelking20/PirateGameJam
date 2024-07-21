The Scene Controller has three areas that users have to interact with.

The first is SceneBlockEnum.cs file inside the SceneController folder.
This file contains an Enum that lists out each of the intented scene blocks. This Enum is used mainly for the Custom Editors for the SceneBlockManager
This Enum needs to be indexed and should start at 0 with no gaps in the indexing.
New Enum Entries can be added to this as needed.

The Second is to make a SceneBlock ScriptableObject.
These should be put inside the SceneController/SceneBlocks folder.
To make a new one, RightClick->Create->SceneBlocks->NewBlockObject
Then, select the new SceneBlock ScriptableObject and inside the Inspector window, there should be an editor that lets you Scenes to be loaded as part of the SceneBlock

---
In order for a scene to show up in the editor dropdown, it needs to be added to the buildsettings list of scenes.
---

The Last thing is the SceneController prefab.
It has a custom editor as well that will show an array the size of the SceneBlockEnum.
This array will be expecting SceneBlock ScriptableObjects.
Drag and Drop the SceneBlock that corresponds to the Enum value.




-Using the SceneController during play-
The SceneController prefab should be present in the scene.'
The SceneBlockManager Assumes that the Sceneblock at index 0 is currently loaded at start.
The SceneBlockManager Script is a singleton that has a public method ChangeSceneBlock(SceneBlockEnum).
Calling that method with a new SceneEnum will inload all scenes in the currently loaded SceneBlock and Load all Scenes associated with the new Sceneblock

